using MainNode.Logic.Compile;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile;

[TestClass]
public class LoopCompilerTests
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

    [TestMethod]
    public void PlusIII()
    {
        _loopCompiler.Compile("int A=1+2");
        var A = _flowRepo.Results.Find(x => x.Name == "A").Value;
    }
    [TestMethod]
    public void MinusIII()
    {
        _loopCompiler.Compile("int B=1-2");
        var B = _flowRepo.Results.Find(x => x.Name == "B").Value;
    }
    [TestMethod]
    public void MultiplyIII()
    {
        _loopCompiler.Compile("int C=2*2");
        var C = _flowRepo.Results.Find(x => x.Name == "C").Value;
    }
    [TestMethod]
    public void DivideIII()
    {
        _loopCompiler.Compile("int D=4/2");
        var D = _flowRepo.Results.Find(x => x.Name == "D").Value;
    }
}
