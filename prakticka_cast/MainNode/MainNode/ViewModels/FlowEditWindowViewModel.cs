using MainNode.Logic.Compile;
using MainNode.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace MainNode.ViewModels
{
    public partial class FlowEditWindowViewModel : ObservableObject
    {
        private readonly NodeListViewModel _nodeList;
        private readonly FlowListViewModel _flowList;
        private readonly FlowRepository _flowRepo;
        private readonly LoopCompiler _loopCompiler;
        public FlowEditWindowViewModel(
            NodeListViewModel nodeList,
            FlowRepository flowRepo,
            FlowListViewModel flowList,
            LoopCompiler loopCompiler)
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
                _loopCompiler.CompileMultyLine(FlowCode);
                MessageBox.Show("compiled successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            _flowList.refreshFlows();
        }

    }
}
