using CommunityToolkit.Mvvm.ComponentModel;
using MainNode.Logic.Interfaces;

namespace MainNode.ViewModels
{
    public partial class NodeListViewModel:ObservableObject
    {
        private readonly INodeRepository _nodeRepo;
        public NodeListViewModel(INodeRepository nodeRepo)
        {
            _nodeRepo = nodeRepo;
        }
        public List<NodeViewModel> Nodes => _nodeRepo.Nodes.Select(x => new NodeViewModel(x, _nodeRepo, this)).ToList();
        public void refreshNodes() => OnPropertyChanged(nameof(Nodes));
    }
}
