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
        private readonly FlowRepository _flowRepo;
        private readonly NodeRepository _nodeRepo;
        private readonly LoopCompiler _loopCompiler;
        public FlowEditWindowViewModel(NodeListViewModel nodeList, FlowRepository flowRepo, NodeRepository nodeRepo, LoopCompiler loopCompiler)
        {
            _nodeList = nodeList;
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
            _loopCompiler = loopCompiler;
        }
        public NodeListViewModel NodeListViewModel => _nodeList;

        [ObservableProperty]
        private string _flowCode = "";

        [RelayCommand]
        public async Task CompileFlow()
        {
            _flowRepo.Clear();
            try
            {
                _loopCompiler.Compile(FlowCode);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
