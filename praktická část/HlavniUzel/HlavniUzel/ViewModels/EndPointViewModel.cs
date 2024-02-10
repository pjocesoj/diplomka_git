using CommunityToolkit.Mvvm.ComponentModel;
using HlavniUzel.Logika.Do;

namespace HlavniUzel.ViewModels
{
    public partial class EndPointViewModel:ObservableObject
    {
        private readonly EndPointDo _endPoint;

        public EndPointViewModel(EndPointDo endPoint)
        {
            this._endPoint = endPoint;

            Address = endPoint.URL;
        }

        [ObservableProperty]
        private string _address = "";
    }
}
