using MainNode.Logic.Compile;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile
{
    [TestClass]
    public partial class LoopCompilerNodeTests
    {
        private LoopCompiler _loopCompiler;
        private FlowRepository _flowRepo;
        private NodeRepository _nodeRepo;
        private MockNode _mockNode;

        [TestInitialize]
        public void Initialize()
        {
            _flowRepo = new FlowRepository();
            _nodeRepo = new NodeRepository();
            var funcRepo = new FuncRepo();
            _loopCompiler = new LoopCompiler(_flowRepo, _nodeRepo, funcRepo);

            var comm = new Communication.HttpNodeCommunication();//dočasně než udělám mock komunikace
            _mockNode = new MockNode(comm);
            _nodeRepo.Nodes.Add(_mockNode.Node);
        }
    }
}
