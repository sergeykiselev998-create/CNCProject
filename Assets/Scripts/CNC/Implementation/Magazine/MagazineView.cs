using CNC.Interfaces.Magazine;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.Magazine
{
    public class MagazineView<TTool> : IMagazineView<TTool>
    where TTool : IMainData
    {
        public IMagazineView<TTool> MainView { get; }
        public IMagazineView<TTool> OffsetView { get; }
        public IMagazineView<TTool> ThirdView { get; }
        public IMagazineView<TTool> FourthView { get; }

        public MagazineView(
            IMagazineView<TTool> mainView,
            IMagazineView<TTool> offsetView,
            IMagazineView<TTool> thirdView,
            IMagazineView<TTool> fourthView)
        {
            MainView = mainView;
            OffsetView = offsetView;
            ThirdView = thirdView;
            FourthView = fourthView;
        }

        public void AddMagazine(int location, TTool tool)
        {
            MainView.AddMagazine(location, tool);
            OffsetView.AddMagazine(location, tool);
            ThirdView.AddMagazine(location, tool);
            FourthView.AddMagazine(location, tool);
        }
    }
}