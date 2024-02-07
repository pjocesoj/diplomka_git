using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HlavniUzel.Logika;

namespace HlavniUzel.ViewModels
{
    public partial class NodeViewModel : ObservableObject
    {
        private Node _node;
        public NodeViewModel(Node node)
        {
            _node = node;

            Name= _node.Name;
            Adr = _node.Address;
        }
        [ObservableProperty]
        private string _name="";

        [ObservableProperty]
        private string _adr = "";

        [RelayCommand]
        public async Task ButtonClick()
        {
            _node.Name = Name;
            _node.Address = Adr;
        }
    }
}
