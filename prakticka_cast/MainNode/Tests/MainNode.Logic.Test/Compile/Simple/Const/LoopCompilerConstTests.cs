using MainNode.Logic.Compile;
using MainNode.Logic.Interfaces;

namespace MainNode.Logic.Test.Compile.Simple.Const;

[TestClass]
public partial class LoopCompilerConstTests
{
    protected ILoopCompiler _loopCompiler;
    protected IFlowRepository _flowRepo;

    [TestInitialize]
    public void Initialize()
    {
        _flowRepo = TestDependencyResolver.Resolve<IFlowRepository>();
        var nodeRepo = TestDependencyResolver.Resolve<INodeRepository>();
        var funcRepo = TestDependencyResolver.Resolve<FuncRepo>();
        _loopCompiler = new LoopCompiler(_flowRepo, nodeRepo, funcRepo);
    }

}
