using CommunityToolkit.Mvvm.ComponentModel;
using MainNode.Logic.Do;

namespace MainNode.ViewModels
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
                return _endPoint.Values.ToStringListShort();
            }
        }
        public List<string> Arguments
        {
            get
            {
                return _endPoint.Arguments.ToStringListShort();
            }
        }
    }
}
