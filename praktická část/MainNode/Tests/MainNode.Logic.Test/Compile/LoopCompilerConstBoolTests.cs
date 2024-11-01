using MainNode.Logic.Compile;
using MainNode.Logic.Do;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile;

[TestClass]
public class LoopCompilerConstBoolTests
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

    #region or
    [TestMethod]
    public void BBB_Plus_FalseTrue()
    {
        // Arrange
        _loopCompiler.Compile("bool A=false+true");
        // Act
        var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, A);
    }
    [TestMethod]
    public void BBB_Or_FalseFalse()
    {
        _loopCompiler.Compile("bool A1=false|false");
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, A1);
    }
    [TestMethod]
    public void BBB_Or_TrueTrue()
    {
        _loopCompiler.Compile("bool A2=true|true");
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A2);
    }
    [TestMethod]
    public void BBB_Or_TrueFalse()
    {
        _loopCompiler.Compile("bool A3=true|false");
        var A3 = (_flowRepo.Results.Find(x => x.Name == "A3").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A3);
    }
    [TestMethod]
    public void BBB_Or_FalseTrue()
    {
        _loopCompiler.Compile("bool A4=false|true");
        var A4 = (_flowRepo.Results.Find(x => x.Name == "A4").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A4);
    }
    #endregion

    #region and
    [TestMethod]
    public void BBB_Multiply_FalseFalse()
    {
        _loopCompiler.Compile("bool B=false*false");
        var B = (_flowRepo.Results.Find(x => x.Name == "B").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, B);
    }
    [TestMethod]
    public void BBB_And_FalseFalse()
    {
        _loopCompiler.Compile("bool B1=false&false");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, B1);
    }
    [TestMethod]
    public void BBB_And_TrueTrue()
    {
        _loopCompiler.Compile("bool B2=true&true");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, B2);
    }
    [TestMethod]
    public void BBB_And_TrueFalse()
    {
        _loopCompiler.Compile("bool B3=true&false");
        var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, B3);
    }
    [TestMethod]
    public void BBB_And_FalseTrue()
    {
        _loopCompiler.Compile("bool B4=false&true");
        var B4 = (_flowRepo.Results.Find(x => x.Name == "B4").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, B4);
    }
    #endregion

    #region not
    [TestMethod]
    public void B_Not_True()
    {
        _loopCompiler.Compile("bool C1=!true");
        var C1 = (_flowRepo.Results.Find(x => x.Name == "C1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, C1);
    }
    [TestMethod]
    public void B_Not_False()
    {
        _loopCompiler.Compile("bool C2=!false");
        var C2 = (_flowRepo.Results.Find(x => x.Name == "C2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, C2);
    }
    #endregion

    #region or not
    [TestMethod]
    public void BBB_OrNot_TrueFalse()
    {
        _loopCompiler.Compile("bool D1=true|!false");
        var D1 = (_flowRepo.Results.Find(x => x.Name == "D1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, D1);
    }
    [TestMethod]
    public void BBB_OrNot_FalseTrue()
    {
        _loopCompiler.Compile("bool D2=false|!true");
        var D2 = (_flowRepo.Results.Find(x => x.Name == "D2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, D2);
    }
    [TestMethod]
    public void BBB_OrNot_FalseFalse()
    {
        _loopCompiler.Compile("bool D3=false|!false");
        var D3 = (_flowRepo.Results.Find(x => x.Name == "D3").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, D3);
    }
    [TestMethod]
    public void BBB_OrNot_TrueTrue()
    {
        _loopCompiler.Compile("bool D4=true|!true");
        var D4 = (_flowRepo.Results.Find(x => x.Name == "D4").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, D4);
    }
    #endregion

    #region and not
    [TestMethod]
    public void BBB_AndNot_TrueFalse()
    {
        _loopCompiler.Compile("bool E1=true&!false");
        var E1 = (_flowRepo.Results.Find(x => x.Name == "E1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, E1);
    }
    [TestMethod]
    public void BBB_AndNot_FalseTrue()
    {
        _loopCompiler.Compile("bool E2=false&!true");
        var E2 = (_flowRepo.Results.Find(x => x.Name == "E2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, E2);
    }
    [TestMethod]
    public void BBB_AndNot_FalseFalse()
    {
        _loopCompiler.Compile("bool E3=false&!false");
        var E3 = (_flowRepo.Results.Find(x => x.Name == "E3").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, E3);
    }
    [TestMethod]
    public void BBB_AndNot_TrueTrue()
    {
        _loopCompiler.Compile("bool E4=true&!true");
        var E4 = (_flowRepo.Results.Find(x => x.Name == "E4").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, E4);
    }
    #endregion
}
