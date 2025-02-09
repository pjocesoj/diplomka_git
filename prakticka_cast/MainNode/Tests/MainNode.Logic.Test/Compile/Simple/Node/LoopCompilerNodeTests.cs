using MainNode.Logic.Compile;
using MainNode.Logic.Interfaces;

namespace MainNode.Logic.Test.Compile.Simple.Node
{
    [TestClass]
    public partial class LoopCompilerNodeTests
    {
        protected LoopCompiler _loopCompiler;
        protected IFlowRepository _flowRepo;
        protected INodeRepository _nodeRepo;
        protected MockNode _mockNode;

        [TestInitialize]
        public void Initialize()
        {
            _flowRepo = TestDependencyResolver.Resolve<IFlowRepository>();
            _nodeRepo = TestDependencyResolver.Resolve<INodeRepository>();
            var funcRepo = TestDependencyResolver.Resolve<FuncRepo>();
            _loopCompiler = new LoopCompiler(_flowRepo, _nodeRepo, funcRepo);

            var comm = new Communication.HttpNodeCommunication();//dočasně než udělám mock komunikace
            _mockNode = new MockNode(comm);
            _nodeRepo.Nodes.Add(_mockNode.Node);
        }
    }
}
