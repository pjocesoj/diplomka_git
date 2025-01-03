using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using MainNode.Windows;
using System.Windows;

namespace MainNode.ViewModels
{
    public partial class NodeViewModel : ObservableObject
    {
        private Node _node;
        private readonly NodeRepository _repo;
        private readonly NodeListViewModel _nodeList;
        public NodeViewModel(Node node, NodeRepository repo, NodeListViewModel nodeList)
        {
            _node = node;
            _repo = repo;
            Name = _node.Name;
            Address = _node.Address;

            _nodeList = nodeList;
        }
        [ObservableProperty]
        private string _name = "";

        [ObservableProperty]
        private string _address = "";

        public ConnectionStatus ConnectionStatus => _node.ConnectionStatus;

        public List<EndPointViewModel> EndPoints
        {
            get
            {
                var ret = new List<EndPointViewModel>();
                foreach (var ep in _node.EndPoints) { ret.Add(new EndPointViewModel(ep)); }
                return ret;
            }
        }

        [RelayCommand]
        public async Task ButtonClick()
        {
            _node.Name = Name;
            _node.Address = Address;

            try
            {
                await _repo.AddNode(_node);
                App.Current.ShowWindow<NodeInfoWindow>(this);
                _nodeList.refreshNodes();
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message, ex); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        [RelayCommand]
        public async Task ShowInfo()
        {
            App.Current.ShowWindow<NodeInfoWindow>(this);
        }
    }
}
