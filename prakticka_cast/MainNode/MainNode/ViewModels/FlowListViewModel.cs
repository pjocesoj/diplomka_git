﻿using CommunityToolkit.Mvvm.ComponentModel;
using MainNode.Logic.Evaluation;
using MainNode.Logic.Interfaces;

namespace MainNode.ViewModels
{
    public partial class FlowListViewModel:ObservableObject
    {
        private readonly IFlowRepository _FlowRepo;
        public FlowListViewModel(IFlowRepository flowRepo)
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
