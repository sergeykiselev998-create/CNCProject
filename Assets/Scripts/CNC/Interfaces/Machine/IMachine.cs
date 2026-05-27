using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Interfaces.Machine
{
    /// <summary>
    /// Interface for the machine model
    /// </summary>
    public interface IMachine<T> where T : IMainData
    {
        IMagazineRepository<T> MagazineRepository { get; }
        T CurrentTool { get; }
        IToolRepository<T> ToolRepository { get; }
    }
}


