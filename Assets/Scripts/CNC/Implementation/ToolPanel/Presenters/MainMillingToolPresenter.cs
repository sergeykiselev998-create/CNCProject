using CNC.Implementation.Tool;
using CNC.Implementation.ToolPanel.Models;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolPanel;

namespace CNC.Implementation.ToolPanel.Presenters
{
    public class MainMillingToolPresenter : MainBasePresenter<MillingToolListModel, IMillingTool>
    {
        public MainMillingToolPresenter(MillingToolListModel model, MainToolPanelView view) 
            : base(model, view)
        {
        }
    }
}