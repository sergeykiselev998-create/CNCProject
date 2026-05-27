namespace CNC.Interfaces.Tool.TurningData
{
    public interface ITurningMagazineData : IToolData
    {
        bool MagazineLocationDisabled { get; set; }
        bool ToolOversize { get; set; }
    }
}