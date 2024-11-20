using MainNode.Logic.Compile;
using MainNode.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNode.ViewModels
{
    public class FlowEditWindowViewModel
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

    }
}
