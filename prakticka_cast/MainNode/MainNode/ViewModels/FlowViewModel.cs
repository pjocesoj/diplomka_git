using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic.Evaluation;
using MainNode.Logic.Interfaces;
using System.Windows;

namespace MainNode.ViewModels
{
    public partial class FlowViewModel : ObservableObject
    {
        private FlowResult _flow;
        private readonly IFlowRepository _repo;
        private readonly FlowListViewModel _flowList;
        public FlowViewModel(FlowResult flow, IFlowRepository repo, FlowListViewModel flowList)
        {
            _flow = flow;
            _repo = repo;
            _flowList = flowList;
        }
        public string Name =>_flow.Name;

        public Type Type => _flow.GetT();


        [RelayCommand]
        public async Task ShowInfo()
        {
            //App.Current.ShowWindow<FlowInfoWindow>(this);
        }
    }
}
