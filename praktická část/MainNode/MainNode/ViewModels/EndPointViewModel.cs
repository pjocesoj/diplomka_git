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
                var values=_endPoint.Values;
                var i = values.Ints.Select(x => x.ToStringShort());
                var f = values.Floats.Select(x => x.ToStringShort());
                var b = values.Bools.Select(x => x.ToStringShort());
                return i.Concat(f).Concat(b).ToList();
            }
        }
    }
}
