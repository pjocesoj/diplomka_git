using MainNode.Logic.Compile;
using MainNode.Logic.Do;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile;

public partial class LoopCompilerTests
{

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
}
