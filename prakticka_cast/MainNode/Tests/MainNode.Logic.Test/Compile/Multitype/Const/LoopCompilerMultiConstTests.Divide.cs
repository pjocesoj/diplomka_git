using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Const;

[TestClass]
public class LoopCompilerMultiConstDivideTests : LoopCompilerMultiConstTests
{
    [TestMethod]
    public void I_I_Divide()
    {
        // Arrange
        _loopCompiler.Compile("int A1=6/2/3");
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(1, A1);
    }
    /*
    [TestMethod]
    public void I_IF_Divide()
    {
        _loopCompiler.Compile("int A2=6/3.0/2");
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<int>).Value;
        Assert.AreEqual(1, A2);
    }
    */
    [TestMethod]
    public void F_F_Divide()
    {
        _loopCompiler.Compile("float B1=7.0/2.0/3.5");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
        Assert.AreEqual(1.0, B1);
    }
    [TestMethod]
    public void F_IF_Divide()
    {
        _loopCompiler.Compile("float B2=7.5/2.5/3");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(1.0, B2);
    }
    [TestMethod]
    public void F_I_Divide()
    {
        _loopCompiler.Compile("float B3=6/2/3");
        var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<float>).Value;
        Assert.AreEqual(1.0, B3);
    }


}
