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
        public ExternalToolConfig MillingExternalToolConfig { get; }
        public ExternalToolConfig TurningExternalToolConfig { get; }
        
        public MagazineConfig MillingMagazineConfig { get; }
        public MagazineConfig TurningMagazineConfig { get; }
        
        public AdditionalToolConfig MillingAdditionalConfig { get; }
        public AdditionalToolConfig TurningAdditionalConfig { get; }
        
        public ConfigProvider()
        {
            const string toolConfigFilePath = "D:\\STEPPER_CNC_AND_SIMULATOR_SRC\\STEPPER_2022\\Config\\";
            var streamingAssetsPath = Application.streamingAssetsPath;

            MillingExternalToolConfig = new ExternalToolConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "tools.ini"),
                MainSection = "Main",
                CountKey = "FtoolCount",
                ToolSectionPrefix = "FTool",
                TitleKey = "Title",
                Param1Key = "Param",
                Param2Key = "Tool"
            };

            TurningExternalToolConfig = new ExternalToolConfig
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
            
            MillingAdditionalConfig = new AdditionalToolConfig
            {
                FilePath = Path.Combine(streamingAssetsPath, "milling_additional.ini"),
                MainSection = "Main",
                CountKey = "Count",
                OffsetDataValueCount = 2,
                MagazineDataBoolValueCount = 3,
                WearDataBoolValueCount = 2,
                OffsetEdgeFloatValueCount = 3,
                OffsetEdgeIntValueCount = 1,
                WearEdgeFloatValueCount = 11
            };

            TurningAdditionalConfig = new AdditionalToolConfig
            {
                FilePath = Path.Combine(toolConfigFilePath, "turning_additional.ini"),
                MainSection = "Main",
                CountKey = "Count",
                OffsetDataValueCount = 2,
                MagazineDataBoolValueCount = 2,
                WearDataBoolValueCount = 2,
                OffsetEdgeFloatValueCount = 6,
                OffsetEdgeIntValueCount = 0,
                WearEdgeFloatValueCount = 12
            };
        }
    }
}