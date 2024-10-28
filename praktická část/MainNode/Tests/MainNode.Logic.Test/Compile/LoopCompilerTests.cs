using MainNode.Logic.Compile;
using MainNode.Logic.Do;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile;

public partial class LoopCompilerTests
{
    private LoopCompiler _loopCompiler;
    private FlowRepository _flowRepo;

    [TestInitialize]
    public void Initialize()
    {
        _flowRepo = new FlowRepository();
        var nodeRepo = new NodeRepository();
        var funcRepo = new FuncRepo();
        _loopCompiler = new LoopCompiler(_flowRepo, nodeRepo, funcRepo);
    }

}
