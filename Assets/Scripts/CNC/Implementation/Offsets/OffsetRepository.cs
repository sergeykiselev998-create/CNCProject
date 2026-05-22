using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.Offsets
{
    /// <summary>
    /// Repository implementation for tool offsets
    /// </summary>
    public class OffsetRepository<T> : IOffsetRepository<T> where T : ITool
    {
        private readonly Dictionary<(int, int), IOffset> _offsets = new();
        private readonly IConfigReader _reader;
        private readonly IConfigWriter _writer;
        private readonly OffsetConfig _config;

        public Dictionary<(int, int), IOffset> Offsets => _offsets;

        public OffsetRepository(OffsetConfig config)
        {
            _config = config;
            _reader = new IniReader(config.FilePath);
            _writer = new IniWriter(config.FilePath);
        }

        public void Load(IEnumerable<int> toolIds)
        {
            _offsets.Clear();
            
            if (!_config.FileExists)
                return;
            
            foreach (var toolId in toolIds)
            {
                var sectionKey = $"{_config.ToolSectionPrefix}{toolId}";
                var edgesCount = _reader.ReadInt(sectionKey, _config.EdgesCountKey, 0);
                
                if (edgesCount == 0) continue;
                
                for (int d = 1; d <= edgesCount; d++)
                {
                    var values = _reader.ReadString(sectionKey, $"{_config.OffsetKeyPrefix}{d}", string.Empty);
                    
                    if (string.IsNullOrEmpty(values)) continue;
                    
                    var parts = values.Split(',');
                    if (parts.Length >= 4)
                    {
                        float x = float.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                        float z = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                        float t = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
                        
                        _offsets[(toolId, d)] = new Offset(x, y, z, t);
                    }
                }
            }
        }
        
        /// <summary>
        /// Загружает ВСЕ офсеты из файла, сканируя секции [T*].
        /// Не требует списка ID.
        /// </summary>
        public void LoadAll()
        {
            _offsets.Clear();
            
            if (!_config.FileExists)
                return;

            string content = File.ReadAllText(_config.FilePath);
            
            // Regex для поиска секций вида [T1], [T25] и т.д.
            // Ищем строки, начинающиеся с [T, затем цифры, затем ]
            var regex = new Regex(@"^\[T(\d+)\]", RegexOptions.Multiline);
            var matches = regex.Matches(content);

            foreach (Match match in matches)
            {
                if (int.TryParse(match.Groups[1].Value, out int toolId))
                {
                    LoadOffsetsForTool(toolId);
                }
            }
        }

        private void LoadOffsetsForTool(int toolId)
        {
            var sectionKey = $"{_config.ToolSectionPrefix}{toolId}";
            var edgesCount = _reader.ReadInt(sectionKey, _config.EdgesCountKey, 0);

            if (edgesCount <= 0) return;

            for (int d = 1; d <= edgesCount; d++)
            {
                var values = _reader.ReadString(sectionKey, $"{_config.OffsetKeyPrefix}{d}", string.Empty);
                if (string.IsNullOrEmpty(values)) continue;

                var parts = values.Split(',');
                if (parts.Length >= 4)
                {
                    if (float.TryParse(parts[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float x) &&
                        float.TryParse(parts[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float y) &&
                        float.TryParse(parts[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float z) &&
                        float.TryParse(parts[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float t))
                    {
                        _offsets[(toolId, d)] = new Offset(x, y, z, t);
                    }
                }
            }
        }

        public void Save()
        {
            try
            {
                var groupedOffsets = new Dictionary<int, Dictionary<int, IOffset>>();
                
                foreach (var kvp in _offsets)
                {
                    var (toolId, offsetNumber) = kvp.Key;
                    var offset = kvp.Value;
                    
                    if (!groupedOffsets.ContainsKey(toolId))
                        groupedOffsets[toolId] = new Dictionary<int, IOffset>();
                    
                    groupedOffsets[toolId][offsetNumber] = offset;
                }
                
                _writer.WriteInt(_config.MainSection, _config.CountKey, groupedOffsets.Count);
                
                foreach (var toolGroup in groupedOffsets)
                {
                    var toolId = toolGroup.Key;
                    var offsets = toolGroup.Value;
                    var sectionKey = $"{_config.ToolSectionPrefix}{toolId}";
                    
                    _writer.WriteInt(sectionKey, _config.EdgesCountKey, offsets.Count);
                    
                    foreach (var offset in offsets)
                    {
                        var d = offset.Key;
                        var value = offset.Value;
                        var valuesString = $"{value.X.ToString(System.Globalization.CultureInfo.InvariantCulture)}," +
                                         $"{value.Y.ToString(System.Globalization.CultureInfo.InvariantCulture)}," +
                                         $"{value.Z.ToString(System.Globalization.CultureInfo.InvariantCulture)}," +
                                         $"{value.T.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
                        
                        _writer.WriteString(sectionKey, $"{_config.OffsetKeyPrefix}{d}", valuesString);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[OffsetRepository] Save error: {ex.Message}");
            }
        }

        public bool TryGetOffset(int toolId, int offsetNumber, out IOffset? offset)
        {
            return _offsets.TryGetValue((toolId, offsetNumber), out offset);
        }

        public Dictionary<int, IOffset> GetOffsetsOrDefault(int toolId)
        {
            var offsets = new Dictionary<int, IOffset>();
    
            foreach (var kvp in _offsets)
            {
                var (id, offsetNumber) = kvp.Key;
                if (id == toolId)
                {
                    offsets[offsetNumber] = kvp.Value;
                }
            }
    
            if (offsets.Count == 0)
            {
                offsets[1] = new Offset();
            }
    
            return offsets;
        }
        
        public void SetOffset(int toolId, int offsetNumber, IOffset offset)
        {
            _offsets[(toolId, offsetNumber)] = offset;
        }

        public void Clear()
        {
            _offsets.Clear();
        }
    }
}