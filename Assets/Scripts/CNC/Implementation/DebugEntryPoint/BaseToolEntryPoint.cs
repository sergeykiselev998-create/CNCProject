using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using CNC.Implementation.Buffer;
using UnityEngine;
using CNC.Implementation.Config;
using CNC.Implementation.Factories;
using CNC.Implementation.Magazine;
using CNC.Implementation.Offsets;
using CNC.Implementation.Slots;
using CNC.Implementation.ToolHolder;
using CNC.Implementation.ToolList;
using CNC.Implementation.ToolPanel;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Buffer;
using CNC.Interfaces.Config;
using CNC.Interfaces.Tool;
using CNC.Interfaces.Magazine;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.ToolHolder;
using CNC.Interfaces.ToolList;
using CNC.Interfaces.ToolPanel;
using CNC.Interfaces.Views;

namespace CNC.Implementation.DebugEntryPoint
{
    /// <summary>
    /// Базовый класс для точки входа конкретного типа инструментов (Milling/Turning).
    /// TTool - интерфейс инструмента (IMillingTool или ITurningTool)
    /// TSerializableTool - сериализуемая версия инструмента
    /// </summary>
    public abstract class BaseToolEntryPoint<TTool, TSerializableTool> : MonoBehaviour 
        where TTool : ITool 
        where TSerializableTool : ISerializableTool
    {
        [Header("Offsets")]
        [SerializedDictionary("Id, Edge", "Offset")]
        public SerializedDictionary<SerializableIntPair, Offset> Offsets = new();

        [Header("Tools")]
        [SerializedDictionary("Id", "Tool Data")]
        public SerializedDictionary<int, TSerializableTool> tools = new();
        
        [Header("Magazine")]
        [SerializedDictionary("Location", "ToolId")]
        public SerializedDictionary<int, int> magazine = new();

        [Header("Buffer")]
        public List<int> buffer = new();
        
        [SerializeField] protected ToolPanelConfig toolPanelConfig;
        
        //Repositories
        protected IConfigProvider ConfigProvider { get; set; }
        protected IOffsetRepository<TTool> OffsetRepo { get; set; }
        protected IToolRepository<TTool> ToolRepo { get; set; }
        protected IMagazineRepository<TTool> MagazineRepo { get; set; }
        protected IBufferRepository<TTool> BufferRepo { get; set; }
        
        //Models
        protected IToolListModel<TTool> ToolListModel { get; set; }
        protected IMagazineModel<TTool> MagazineModel { get; set; }
        protected IBufferModel<TTool> BufferModel { get; set; }
        
        protected IToolHolderModel<TTool> ToolHolderModel { get; set; }

        //Presenters
        protected IToolHolderPresenter<TTool> ToolHolderPresenter { get; set; }
        protected IMagazinePresenter<TTool> MagazinePresenter { get; set; }
        protected IBufferPresenter<TTool> BufferPresenter { get; set; }



        protected void Initialize()
        {
            ConfigProvider = new ConfigProvider();
            
            InitializeRepositories();
            InitializeModels();
            InitializePresenters();
            
            LoadData();
            UpdateSerializedFields();

            InitializeUI();
        }

        protected abstract void InitializeRepositories();

        protected abstract void InitializePresenters();

        private void InitializeModels()
        {
            ToolListModel = new ToolListModel<TTool>(ToolRepo);
            MagazineModel = new MagazineModel<TTool>(MagazineRepo);
            BufferModel = new BufferModel<TTool>(BufferRepo);
            ToolHolderModel = new ToolHolderModel<TTool>(ToolRepo);
        }

        private void InitializeUI()
        {
            ToolHolderPresenter.Initialize();
            MagazinePresenter.Initialize();
            BufferPresenter.Initialize();
        }
        
        private void LoadData()
        {
            OffsetRepo.LoadAll();
            ToolRepo.Load();
            LoadBuffer();
        }

        private void LoadBuffer()
        {
            var bufferTools = ToolRepo.Tools.Keys
                .Where(id => !MagazineRepo.Slots.ContainsValue(id));
            BufferRepo.SetTools(bufferTools);
        }
        
        private void UpdateSerializedFields()
        {
            CopyOffsets();
            CopyTools();
            CopyMagazine();
            CopyBuffer();
        }

        private void CopyOffsets()
        {
            Offsets.Clear();

            foreach (var kvp in OffsetRepo.Offsets)
            {
                var key = new SerializableIntPair(kvp.Key.Item1, kvp.Key.Item2);
                Offsets[key] = kvp.Value as Offset;
            }
        }
        
        protected void CopyMagazine()
        {
            magazine.Clear();
            foreach (var slot in MagazineRepo.Slots)
            {
                magazine[slot.Key] = slot.Value;
            }
        }

        private void CopyTools()
        {
            tools.Clear();
            foreach (var kvp in ToolRepo.Tools)
            {
                if (kvp.Value != null)
                {
                    tools[kvp.Key] = ConvertToSerializable(kvp.Value);
                }
            }
        }
        
        private void CopyBuffer()
        {
            buffer.Clear();
            foreach (var slot in BufferRepo.Slots)
            {
                buffer.Add(slot);
            }
        }

        protected abstract TSerializableTool ConvertToSerializable(TTool tool);
    }
}