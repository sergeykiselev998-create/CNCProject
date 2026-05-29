using System;
using CNC.Implementation.Buffer;
using CNC.Implementation.Controls;
using CNC.Implementation.Factories;
using CNC.Implementation.Magazine;
using CNC.Implementation.Slots;
using CNC.Implementation.Strategies;
using CNC.Implementation.ToolHolder;
using CNC.Implementation.ToolPanel.Models;
using CNC.Implementation.ToolPanel.Presenters;
using CNC.Implementation.ToolPanel.Repositories;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Factories;
using CNC.Interfaces.Tool;
using Reflex.Core;
using UnityEngine;

namespace CNC.Implementation.DebugEntryPoint
{
    public class MillingEntryPoint : BaseToolEntryPoint<IMillingTool, SerializableMillingTool>, IInstaller
    {
        [Header("ToolPanel")] 
        [SerializeField] private MainToolPanelView mainView;
        [SerializeField] private MillingOffsetPanelView offsetView;
        [SerializeField] private ThirdToolPanelView thirdView;
        [SerializeField] private FourthToolPanelView fourthView;

        private MainMillingToolPresenter _mainPresenter;
        private OffsetMillingToolPresenter _offsetPresenter;

        private MillingToolListModel _toolListModel;
        
        [ContextMenu("Initialize")]
        private void Init()
        {
            Initialize();
        }
        
        protected override void InitializeRepositories()
        {
            var toolConfig = ConfigProvider.MillingExternalToolConfig;
            var additionalDataConfig = ConfigProvider.MillingAdditionalConfig;

            var externalRepository = new MillingExternalToolRepository(toolConfig);
            var additionalRepository = new MillingAdditionalToolRepository(additionalDataConfig);
            
            var toolRepo = new MillingToolRepository(externalRepository, additionalRepository);
            ToolRepository = toolRepo;

            var magConfig = ConfigProvider.MillingMagazineConfig;
            var magRepo = new MagazineRepository<IMillingTool>(toolRepo, magConfig);
            MagazineRepo = magRepo;

            var bufRepo = new BufferRepository<IMillingTool>(toolRepo);
            BufferRepo = bufRepo;
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            var mainSlotFactory = new SlotFactory<MainSlotControl, MainSlot, IMainData>(toolPanelConfig.MainSlotPrefab, toolPanelConfig.MainSlotControl);
            var offsetSlotFactory = new SlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>(toolPanelConfig.MillingOffsetPrefab, toolPanelConfig.MillingOffsetSlotControl);
            var thirdSlotFactory = new SlotFactory<ThirdSlotControl, ThirdSlot, IMainData>(toolPanelConfig.ThirdSlotPrefab, toolPanelConfig.ThirdSlotControl);
            var fourthSlotFactory = new SlotFactory<FourthSlotControl, FourthSlot, IMainData>(toolPanelConfig.FourthSlotPrefab, toolPanelConfig.FourthSlotControl);

            containerBuilder.RegisterValue(mainSlotFactory, new[] 
            { 
                typeof(SlotFactory<MainSlotControl, MainSlot, IMainData>),
                typeof(ISlotFactory<MainSlotControl, MainSlot, IMainData>)
            });
    
            containerBuilder.RegisterValue(offsetSlotFactory, new[] 
            { 
                typeof(SlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>),
                typeof(ISlotFactory<MillingOffsetSlotControl, MillingOffsetSlot, IMillingTool>)
            });
    
            containerBuilder.RegisterValue(thirdSlotFactory, new[] 
            { 
                typeof(SlotFactory<ThirdSlotControl, ThirdSlot, IMainData>),
                typeof(ISlotFactory<ThirdSlotControl, ThirdSlot, IMainData>)
            });
    
            containerBuilder.RegisterValue(fourthSlotFactory, new[] 
            { 
                typeof(SlotFactory<FourthSlotControl, FourthSlot, IMainData>),
                typeof(ISlotFactory<FourthSlotControl, FourthSlot, IMainData>)
            });
        }
        
        protected override void InitializePresenters()
        {
          var toolHolderView = new ToolHolderView<IMillingTool>(mainView, offsetView, thirdView, fourthView);
          var toolHolderStrategy = new ToolHolderMillingStrategy();
          ToolHolderPresenter = new ToolHolderPresenter<IMillingTool>(ToolHolderModel, toolHolderView,toolHolderStrategy);
          
          var magazineView = new MagazineView<IMillingTool>(mainView, offsetView, thirdView, fourthView);
          MagazinePresenter = new MagazinePresenter<IMillingTool>(MagazineModel,magazineView);
          
          var bufferView = new BufferView<IMillingTool>(mainView, offsetView, thirdView, fourthView);
          BufferPresenter = new BufferPresenter<IMillingTool>(BufferModel,bufferView);

          _mainPresenter = new MainMillingToolPresenter(_toolListModel, mainView);
          _offsetPresenter = new OffsetMillingToolPresenter(_toolListModel, offsetView);
        }

        protected override void InitializeModels()
        {
            base.InitializeModels();
            
            _toolListModel = new MillingToolListModel(ToolRepository);
        }

        protected override SerializableMillingTool ConvertToSerializable(IMillingTool tool)
        {
            return SerializableMillingTool.FromInterface(tool);
        }

        private void OnApplicationQuit()
        {
             ToolRepository.SaveAdditional();
        }
    }
}