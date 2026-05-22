using CNC.Interfaces.Tool;

namespace CNC.Interfaces.Strategies
{
    public interface IToolHolderStrategy
    { 
        bool IsUIAllowed { get; }
    }
}