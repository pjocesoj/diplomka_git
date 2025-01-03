using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Const;

[TestClass]
public class LoopCompilerMultiConstMultiplyTests : LoopCompilerMultiConstTests
{
    [TestMethod]
    public void I_I_Multiply()
    {
        // Arrange
        _loopCompiler.Compile("int A1=1*2*3");
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(6, A1);
    }
    /*
    [TestMethod]
    public void I_IF_Multiply()
    {
        _loopCompiler.Compile("int A2=1*2.5*3");
        var A2=(_flowRepo.Results.Find(x=>x.Name=="A2").Value as ValueDo<int>).Value;
        Assert.AreEqual(7,A2);
    }
    */
    [TestMethod]
    public void F_F_Multiply()
    {
        _loopCompiler.Compile("float B1=1.0*2.0*3.5");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
        Assert.AreEqual(7.0, B1);
    }
    [TestMethod]
    public void F_IF_Multiply()
    {
        _loopCompiler.Compile("float B2=1*2.5*3");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(7.5, B2);
    }
    [TestMethod]
    public void F_I_Multiply()
    {
        _loopCompiler.Compile("float B3=1*2*3");
        var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<float>).Value;
        Assert.AreEqual(6, B3);
    }
    [TestMethod]
    public void B_B_Multiply()
    {
        _loopCompiler.Compile("bool C1=true*false*true");
        var C1 = (_flowRepo.Results.Find(x => x.Name == "C1").Value as ValueDo<bool>).Value;
        Assert.AreEqual(false, C1);
    }
}
