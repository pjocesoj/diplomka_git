using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using MainNode.Logic.Evaluation;
using MainNode.Windows;
using System.Windows;

namespace MainNode.ViewModels
{
    public partial class FlowViewModel : ObservableObject
    {
        private FlowResult _flow;
        private readonly FlowRepository _repo;
        private readonly FlowListViewModel _flowList;
        public FlowViewModel(FlowResult flow, FlowRepository repo, FlowListViewModel flowList)
        {
            _flow = flow;
            _repo = repo;
            _flowList = flowList;
        }
        public string Name =>_flow.Name;

        public Type Type => _flow.getT();


        [RelayCommand]
        public async Task ShowInfo()
        {
            //App.Current.ShowWindow<FlowInfoWindow>(this);
        }
    }
}
