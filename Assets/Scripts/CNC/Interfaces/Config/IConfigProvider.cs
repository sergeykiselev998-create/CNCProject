using CNC.Implementation.Config;

namespace CNC.Interfaces.Config
{
    public interface IConfigProvider
    {
        OffsetConfig MillingOffsetConfig { get; }
        OffsetConfig TurningOffsetConfig { get; }
        ToolConfig MillingToolConfig { get; }
        ToolConfig TurningToolConfig { get; }
        MagazineConfig MillingMagazineConfig { get; }
        MagazineConfig TurningMagazineConfig { get; }
    }
}