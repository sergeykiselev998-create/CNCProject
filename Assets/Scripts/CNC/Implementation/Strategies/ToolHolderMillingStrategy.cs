using CNC.Interfaces.Strategies;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.Strategies
{
    public class ToolHolderMillingStrategy : IToolHolderStrategy
    {
        public bool IsUIAllowed { get; } = true;
    }
}