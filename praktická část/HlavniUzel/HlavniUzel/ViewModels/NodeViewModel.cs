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
        }
        [ObservableProperty]
        private string _name="a";
        //public string Name=>_node.Name;

        [RelayCommand]
        public async Task ButtonClick()
        {
            string n = Name;
            int a = 0;
        }
    }
}
