using MainNode.Logic.Compile;

namespace MainNode.Logic.Test.Compile.Simple.Const;

[TestClass]
public partial class LoopCompilerConstTests
{
    protected LoopCompiler _loopCompiler;
    protected FlowRepository _flowRepo;

    [TestInitialize]
    public void Initialize()
    {
        _flowRepo = new FlowRepository();
        var nodeRepo = new NodeRepository();
        var funcRepo = new FuncRepo();
        _loopCompiler = new LoopCompiler(_flowRepo, nodeRepo, funcRepo);
    }

}
