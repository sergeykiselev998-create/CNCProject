namespace CNC.Interfaces.Tool.MillingData
{
    public interface IMillingMagazineData : IToolData
    {
        bool MagazineLocationDisabled { get; set; }
        bool ToolOversize { get; set; }
        bool ToolOnFixedLocation { get; set; }
    }
}