using CNC.Interfaces.Magazine;
using CNC.Interfaces.Offsets;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolList;

namespace CNC.Interfaces.Machine
{
    /// <summary>
    /// Interface for the machine model
    /// </summary>
    public interface IMachine<T> where T : ITool
    {
        IMagazineRepository<T> MagazineRepository { get; }
        T? CurrentTool { get; }
        IOffsetRepository<T> OffsetRepository { get; }
        IToolRepository<T> ToolRepository { get; }
    }
}


