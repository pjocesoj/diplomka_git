using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using MainNode.Windows;
using System.Net;
using System.Windows;
using MainNode.Logic.Evaluation;

namespace MainNode.ViewModels
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
        private string _name = "";

        [ObservableProperty]
        private string _address = "";

        public List<EndPointViewModel> EndPoints 
        {
            get
            {
                var ret=new List<EndPointViewModel>();
                foreach(var ep in _node.EndPoints) { ret.Add(new EndPointViewModel(ep)); }
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
            }
            catch(ApplicationException ex) { throw new ApplicationException(ex.Message,ex); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
