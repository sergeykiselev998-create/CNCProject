using CNC.Interfaces.Strategies;

namespace CNC.Implementation.Strategies
{
    public class ToolHolderTurningStrategy : IToolHolderStrategy
    {
        public bool IsUIAllowed { get; } = false;
    }
}