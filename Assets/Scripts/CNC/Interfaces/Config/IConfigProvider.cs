using CNC.Implementation.Config;

namespace CNC.Interfaces.Config
{
    public interface IConfigProvider
    {
        AdditionalToolConfig MillingAdditionalConfig { get; }
        AdditionalToolConfig TurningAdditionalConfig { get; }
        ExternalToolConfig MillingExternalToolConfig { get; }
        ExternalToolConfig TurningExternalToolConfig { get; }
        MagazineConfig MillingMagazineConfig { get; }
        MagazineConfig TurningMagazineConfig { get; }
    }
}