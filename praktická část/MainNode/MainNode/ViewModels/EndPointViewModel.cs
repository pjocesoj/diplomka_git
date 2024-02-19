using CommunityToolkit.Mvvm.ComponentModel;
using MainNode.Logic.Do;

namespace HlavniUzel.ViewModels
{
    public partial class EndPointViewModel : ObservableObject
    {
        private readonly EndPointDo _endPoint;

        public EndPointViewModel(EndPointDo endPoint)
        {
            this._endPoint = endPoint;

            Address = endPoint.Path.ToString();
        }

        [ObservableProperty]
        private string _address = "";

        public List<string> Values
        {
            get
            {
                var i = _endPoint.Ints.Select(x => x.ToStringShort());
                var f = _endPoint.Flots.Select(x => x.ToStringShort());
                var b = _endPoint.Bools.Select(x => x.ToStringShort());
                return i.Concat(f).Concat(b).ToList();
            }
        }
    }
}
