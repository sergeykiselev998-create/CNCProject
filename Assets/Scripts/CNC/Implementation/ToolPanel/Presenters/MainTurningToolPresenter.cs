using CNC.Implementation.ToolPanel.Models;
using CNC.Implementation.ToolPanel.Views;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Presenters
{
    public class MainTurningToolPresenter : MainBasePresenter<TurningToolListModel, ITurningTool>
    {
        public MainTurningToolPresenter(TurningToolListModel model, MainToolPanelView view) 
            : base(model, view)
        {
        }
    }
}