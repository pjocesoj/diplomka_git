using MainNode.Logic.Compile;
using MainNode.Logic.Do;
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
        // Arrange
        _loopCompiler.Compile("int A=1+2");
        // Act
        var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(3, A);
    }
    [TestMethod]
    public void MinusIII()
    {
        _loopCompiler.Compile("int B=1-2");
        var B = (_flowRepo.Results.Find(x => x.Name == "B").Value as ValueDo<int>).Value;
        Assert.AreEqual(-1, B);
    }
    [TestMethod]
    public void MultiplyIII()
    {
        _loopCompiler.Compile("int C=2*2");
        var C = (_flowRepo.Results.Find(x => x.Name == "C").Value as ValueDo<int>).Value;
        Assert.AreEqual(4, C);
    }
    [TestMethod]
    public void DivideIII()
    {
        _loopCompiler.Compile("int D=4/2");
        var D = (_flowRepo.Results.Find(x => x.Name == "D").Value as ValueDo<int>).Value;
        Assert.AreEqual(2, D);
    }
}
