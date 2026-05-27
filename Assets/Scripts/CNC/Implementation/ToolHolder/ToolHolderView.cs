using CNC.Interfaces.Tool;
using CNC.Interfaces.ToolHolder;

namespace CNC.Implementation.ToolHolder
{
    public class ToolHolderView<TTool>: IToolHolderView<TTool>
    where TTool : IMainData
    {
        public IToolHolderView<TTool> MainView { get; }       
        public IToolHolderView<TTool> OffsetView{ get; }  
        public IToolHolderView<TTool> ThirdView{ get; }         
        public IToolHolderView<TTool> FourthView{ get; }  

        public ToolHolderView(
            IToolHolderView<TTool> mainView,        
            IToolHolderView<TTool> offsetView,
            IToolHolderView<TTool> thirdView,       
            IToolHolderView<TTool> fourthView)
        {
            MainView = mainView;        
            OffsetView = offsetView;
            ThirdView = thirdView;        
            FourthView = fourthView;
        }

        public void AddToolHolder(int location, TTool tool)
        {
            MainView.AddToolHolder(location, tool);        
            OffsetView.AddToolHolder(location, tool); 
            ThirdView.AddToolHolder(location, tool);        
            FourthView.AddToolHolder(location, tool); 
        }
    }
}