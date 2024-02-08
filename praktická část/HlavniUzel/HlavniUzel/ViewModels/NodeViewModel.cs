using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HlavniUzel.Logika;
using System.Net;

namespace HlavniUzel.ViewModels
{
    public partial class NodeViewModel : ObservableObject
    {
        private Node _node;
        private readonly NodeRepository _repo;

        public NodeViewModel(Node node, NodeRepository repo)
        {
            _node = node;
            _repo = repo;
            Name = _node.Name;
            Address = _node.Address;
        }
        [ObservableProperty]
        private string _name="";

        [ObservableProperty]
        private string _address = "";

        [RelayCommand]
        public async Task ButtonClick()
        {
            _node.Name = Name;
            _node.Address = Address;

            await _repo.AddNode(_node);

        }
    }
}
