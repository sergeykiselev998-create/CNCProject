using CNC.Interfaces.Buffer;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.Buffer
{
    public class BufferView<TTool> : IBufferView<TTool>
    where TTool : ITool
    {
        public IBufferView<TTool> MainView { get; }       
        public IBufferView<TTool> OffsetView{ get; }  
        public IBufferView<TTool> ThirdView{ get; }         
        public IBufferView<TTool> FourthView{ get; }  

        public BufferView(
            IBufferView<TTool> mainView,        
            IBufferView<TTool> offsetView,
            IBufferView<TTool> thirdView,       
            IBufferView<TTool> fourthView)
        {
            MainView = mainView;        
            OffsetView = offsetView;
            ThirdView = thirdView;        
            FourthView = fourthView;
        }

        public void AddBuffer(int location, TTool tool)
        {
            MainView.AddBuffer(location, tool);        
            OffsetView.AddBuffer(location, tool); 
            ThirdView.AddBuffer(location, tool);        
            FourthView.AddBuffer(location, tool); 
        }
    }
}