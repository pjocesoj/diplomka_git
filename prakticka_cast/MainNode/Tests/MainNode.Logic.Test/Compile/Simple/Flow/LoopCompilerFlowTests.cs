using MainNode.Logic.Compile;
using MainNode.Logic.Interfaces;

namespace MainNode.Logic.Test.Compile.Simple.Flow;

[TestClass]
public partial class LoopCompilerFlowTests
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

        _loopCompiler.Compile("int i=1+2");
        _loopCompiler.Compile("float f=1.5+0.5");
        _loopCompiler.Compile("bool b=true+true");
    }

}
