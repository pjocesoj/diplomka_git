using MainNode.Logic.Compile;

namespace MainNode.Logic.Test.Compile.Simple.Node
{
    [TestClass]
    public partial class LoopCompilerNodeTests
    {
        protected LoopCompiler _loopCompiler;
        protected FlowRepository _flowRepo;
        protected NodeRepository _nodeRepo;
        protected MockNode _mockNode;

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
