using CNC.Interfaces.Tool;

namespace CNC.Interfaces.ToolHolder
{
    public interface IToolHolderView<in TTool>
        where TTool : IMainData
    {
        void AddToolHolder(int location, TTool tool);
    }
}