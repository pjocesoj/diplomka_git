using CommunityToolkit.Mvvm.ComponentModel;
using MainNode.Logic;
using MainNode.Logic.Evaluation;

namespace MainNode.ViewModels
{
    public partial class FlowListViewModel:ObservableObject
    {
        private readonly FlowRepository _FlowRepo;
        public FlowListViewModel(FlowRepository flowRepo)
        {
            _FlowRepo = flowRepo;
        }
        public List<FlowViewModel> Flows => _FlowRepo.Results.Select(x => new FlowViewModel(x, _FlowRepo, this)).ToList();

        /*
        public List<FlowViewModel> Ints => _FlowRepo.Results
                    .Where(x => x is FlowResult<int>)
                    .Select(x => new FlowViewModel(x, _FlowRepo, this))
                    .ToList();
        */

        public void refreshFlows() => OnPropertyChanged(nameof(Flows));
    }
}
