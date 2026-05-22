using System;
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
using CNC.Interfaces.Factories;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = UnityEngine.Resolution;

namespace CNC.Implementation.DebugEntryPoint
{
    public class MillingEntryPoint : BaseToolEntryPoint<IMillingTool, SerializableMillingTool>, IInstaller
    {
        [Header("ToolPanel")] 
        [SerializeField] protected MainToolPanelView mainView;
        [SerializeField] protected MillingOffsetPanelView millingOffsetView;
        [SerializeField] protected ThirdToolPanelView thirdView;
        [SerializeField] protected FourthToolPanelView fourthView;
        
        [ContextMenu("Initialize")]
        private void Init()
        {
            Initialize();
        }
        
        protected override void InitializeRepositories()
        {
            var offsetConfig = ConfigProvider.MillingOffsetConfig;
            var offsetRepo = new OffsetRepository<IMillingTool>(offsetConfig);
            OffsetRepo = offsetRepo;

            var toolConfig = ConfigProvider.MillingToolConfig;
            var toolRepo = new MillingToolRepository(toolConfig, offsetRepo);
            ToolRepo = toolRepo;

            var magConfig = ConfigProvider.MillingMagazineConfig;
            var magRepo = new MagazineRepository<IMillingTool>(toolRepo, magConfig);
            MagazineRepo = magRepo;

            var bufRepo = new BufferRepository<IMillingTool>(toolRepo);
            BufferRepo = bufRepo;
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            var mainSlotFactory = new SlotFactory<MainSlotControl, MainSlot, ITool>(toolPanelConfig.MainSlotPrefab, toolPanelConfig.MainSlotControl);
            var offsetSlotFactory = new SlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>(toolPanelConfig.MillingOffsetPrefab, toolPanelConfig.MillingOffsetSlotControl);
            var thirdSlotFactory = new SlotFactory<ThirdSlotControl, ThirdSlot, ITool>(toolPanelConfig.ThirdSlotPrefab, toolPanelConfig.ThirdSlotControl);
            var fourthSlotFactory = new SlotFactory<FourthSlotControl, FourthSlot, ITool>(toolPanelConfig.FourthSlotPrefab, toolPanelConfig.FourthSlotControl);

            containerBuilder.RegisterValue(mainSlotFactory, new[] 
            { 
                typeof(SlotFactory<MainSlotControl, MainSlot, ITool>),
                typeof(ISlotFactory<MainSlotControl, MainSlot, ITool>)
            });
    
            containerBuilder.RegisterValue(offsetSlotFactory, new[] 
            { 
                typeof(SlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>),
                typeof(ISlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>)
            });
    
            containerBuilder.RegisterValue(thirdSlotFactory, new[] 
            { 
                typeof(SlotFactory<ThirdSlotControl, ThirdSlot, ITool>),
                typeof(ISlotFactory<ThirdSlotControl, ThirdSlot, ITool>)
            });
    
            containerBuilder.RegisterValue(fourthSlotFactory, new[] 
            { 
                typeof(SlotFactory<FourthSlotControl, FourthSlot, ITool>),
                typeof(ISlotFactory<FourthSlotControl, FourthSlot, ITool>)
            });
        }
        
        protected override void InitializePresenters()
        {
          //  ToolPanelPresenter = new MillingToolPanelPresenter(ToolHolder, MagazineModel, BufferModel, toolPanelView, SlotFactory);


          var toolHolderView = new ToolHolderView<IMillingTool>(mainView, millingOffsetView, thirdView, fourthView);
          var toolHolderStrategy = new ToolHolderMillingStrategy();
          ToolHolderPresenter = new ToolHolderPresenter<IMillingTool>(ToolHolderModel, toolHolderView,toolHolderStrategy);
          
          var magazineView = new MagazineView<IMillingTool>(mainView, millingOffsetView, thirdView, fourthView);
          MagazinePresenter = new MagazinePresenter<IMillingTool>(MagazineModel,magazineView);
          
          var bufferView = new BufferView<IMillingTool>(mainView, millingOffsetView, thirdView, fourthView);
          BufferPresenter = new BufferPresenter<IMillingTool>(BufferModel,bufferView);

        }


        protected override SerializableMillingTool ConvertToSerializable(IMillingTool tool)
        {
            return SerializableMillingTool.FromInterface(tool);
        }
        
    }
}