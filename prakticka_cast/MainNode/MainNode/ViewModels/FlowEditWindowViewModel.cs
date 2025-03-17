using MainNode.Logic.Compile;

using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using MainNode.Logic.Interfaces;

namespace MainNode.ViewModels
{
    public partial class FlowEditWindowViewModel : ObservableObject
    {
        private readonly NodeListViewModel _nodeList;
        private readonly FlowListViewModel _flowList;
        private readonly IFlowRepository _flowRepo;
        private readonly ILoopCompiler _loopCompiler;
        public FlowEditWindowViewModel(
            NodeListViewModel nodeList,
            IFlowRepository flowRepo,
            FlowListViewModel flowList,
            ILoopCompiler loopCompiler)
        {
            _nodeList = nodeList;
            _flowList = flowList;
            _flowRepo = flowRepo;
            _loopCompiler = loopCompiler;
        }
        public NodeListViewModel NodeListViewModel => _nodeList;
        public FlowListViewModel FlowListViewModel => _flowList;

        [ObservableProperty]
        private string _flowCode = "";

        [RelayCommand]
        public async Task CompileFlow()
        {
            _flowRepo.Clear();
            try
            {
                _loopCompiler.CompileMultiLine(FlowCode);
                MessageBox.Show("compiled successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            _flowList.RefreshFlows();
        }

    }
}
