using CNC.Implementation.Buffer;
using CNC.Implementation.Controls;
using CNC.Implementation.Factories;
using CNC.Implementation.Magazine;
using CNC.Implementation.Offsets;
using CNC.Implementation.Slots;
using CNC.Implementation.Strategies;
using CNC.Implementation.ToolHolder;
using CNC.Implementation.ToolPanel;
using CNC.Implementation.ToolList;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Tool;
using UnityEngine;

namespace CNC.Implementation.DebugEntryPoint
{
    public class TurningEntryPoint : BaseToolEntryPoint<ITurningTool, SerializableTurningTool>
    {
        [Header("ToolPanel")] 
        [SerializeField] protected MainToolPanelView mainView;
        [SerializeField] protected TurningOffsetPanelView turningOffsetView;
        [SerializeField] protected ThirdToolPanelView thirdView;
        [SerializeField] protected FourthToolPanelView fourthView;
                
        [ContextMenu("Initialize")]
        private void Init()
        {
            Initialize();
        }
        
        protected override void InitializeRepositories()
        {
            var offsetConfig = ConfigProvider.TurningOffsetConfig;
            var offsetRepo = new OffsetRepository<ITurningTool>(offsetConfig);
            OffsetRepo = offsetRepo;

            var toolConfig = ConfigProvider.TurningToolConfig;
            var toolRepo = new TurningToolRepository(toolConfig, offsetRepo);
            ToolRepo = toolRepo;

            var magConfig = ConfigProvider.TurningMagazineConfig;
            var magRepo = new MagazineRepository<ITurningTool>(toolRepo, magConfig);
            MagazineRepo = magRepo;
            
            var bufRepo = new BufferRepository<ITurningTool>(toolRepo);
            BufferRepo = bufRepo;
        }

        protected override void InitializePresenters()
        {
            var mainSlotFactory = new SlotFactory<MainSlotControl, MainSlot, ITool>(toolPanelConfig.MainSlotPrefab, toolPanelConfig.MainSlotControl);
            var offsetSlotFactory = new SlotFactory<TurningOffsetSlotControl, TurningOffsetSlot, ITurningTool>(toolPanelConfig.TurningOffsetPrefab, toolPanelConfig.TurningOffsetSlotControl);
            var thirdSlotFactory = new SlotFactory<ThirdSlotControl, ThirdSlot, ITool>(toolPanelConfig.ThirdSlotPrefab, toolPanelConfig.ThirdSlotControl);
            var fourthSlotFactory = new SlotFactory<FourthSlotControl, FourthSlot, ITool>(toolPanelConfig.FourthSlotPrefab, toolPanelConfig.FourthSlotControl);
            
            var toolHolderView = new ToolHolderView<ITurningTool>(mainView, turningOffsetView, thirdView, fourthView);
            var toolHolderStrategy = new ToolHolderTurningStrategy();
            ToolHolderPresenter = new ToolHolderPresenter<ITurningTool>(ToolHolderModel, toolHolderView,toolHolderStrategy);
          
            var magazineView = new MagazineView<ITurningTool>(mainView, turningOffsetView, thirdView, fourthView);
            MagazinePresenter = new MagazinePresenter<ITurningTool>(MagazineModel,magazineView);
          
            var bufferView = new BufferView<ITurningTool>(mainView, turningOffsetView, thirdView, fourthView);
            BufferPresenter = new BufferPresenter<ITurningTool>(BufferModel,bufferView);
        }
        
        protected override SerializableTurningTool ConvertToSerializable(ITurningTool tool)
        {
            return SerializableTurningTool.FromInterface(tool);
        }
    }
}