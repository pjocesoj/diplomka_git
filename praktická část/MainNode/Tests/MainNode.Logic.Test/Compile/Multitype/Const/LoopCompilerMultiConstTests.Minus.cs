using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Const;

[TestClass]
public class LoopCompilerMultiConstMinusTests : LoopCompilerMultiConstTests
{
    [TestMethod]
    public void I_I_Minus()
    {
        // Arrange
        _loopCompiler.Compile("int A1=1-2-3");
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(-4, A1);
    }
    /*
    [TestMethod]
    public void I_IF_Minus()
    {
        _loopCompiler.Compile("int A2=1-2.5-3");
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<int>).Value;
        Assert.AreEqual(-4.5, A2);
    }
    */
    [TestMethod]
    public void F_F_Minus()
    {
        _loopCompiler.Compile("float B1=1.0-2.0-3.5");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
        Assert.AreEqual(-4.5, B1);
    }
    [TestMethod]
    public void F_IF_Minus()
    {
        _loopCompiler.Compile("float B2=1-2.5-3");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(-4.5, B2);
    }
    [TestMethod]
    public void F_I_Minus()
    {
        _loopCompiler.Compile("float B3=1-2-3");
        var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<float>).Value;
        Assert.AreEqual(-4, B3);
    }

}
