using System;
using System.IO;
using CNC.Implementation.Config;
using CNC.Interfaces.Config;
using UnityEditor;
using UnityEngine;

namespace CNC.Implementation.Config
{
    public class ConfigProvider : IConfigProvider
    {
        public OffsetConfig MillingOffsetConfig { get; }
        public OffsetConfig TurningOffsetConfig { get; }
        public ToolConfig MillingToolConfig { get; }
        public ToolConfig TurningToolConfig { get; }
        public MagazineConfig MillingMagazineConfig { get; }
        public MagazineConfig TurningMagazineConfig { get; }
        
        public ConfigProvider()
        {
            const string toolConfigFilePath = "D:\\STEPPER_CNC_AND_SIMULATOR_SRC\\STEPPER_2022\\Config\\";
            var offsetFilePath = Application.streamingAssetsPath;
            
            MillingOffsetConfig = new OffsetConfig
            {
                FilePath = Path.Combine(offsetFilePath, "millingOffsets.ini"),
                MainSection = "Main",
                CountKey = "Count",
                ToolSectionPrefix = "T",
                EdgesCountKey = "EdgesCount",
                OffsetKeyPrefix = "D"
            };
            
            TurningOffsetConfig = new OffsetConfig
            {
                FilePath = Path.Combine(offsetFilePath, "turningOffsets.ini"),
                MainSection = "Main",
                CountKey = "Count",
                ToolSectionPrefix = "T",
                EdgesCountKey = "EdgesCount",
                OffsetKeyPrefix = "D"
            };

            MillingToolConfig = new ToolConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "tools.ini"),
                MainSection = "Main",
                CountKey = "FtoolCount",
                ToolSectionPrefix = "FTool",
                TitleKey = "Title",
                Param1Key = "Param",
                Param2Key = "Tool"
            };

            TurningToolConfig = new ToolConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "tools.ini"),
                MainSection = "Main",
                CountKey = "toolCount",
                ToolSectionPrefix = "Tool",
                TitleKey = "Title",
                Param1Key = "ShiftX",
                Param2Key = "ShiftY"
            };

            MillingMagazineConfig = new MagazineConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "magazin.ini"),
                MainSection = "Main",
                CountKey = "FCount",
                PositionKeyPrefix = "FPosition"
            };

            TurningMagazineConfig = new MagazineConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "magazin.ini"),
                MainSection = "Main",
                CountKey = "Count",
                PositionKeyPrefix = "Position"
            };
        }
    }
}