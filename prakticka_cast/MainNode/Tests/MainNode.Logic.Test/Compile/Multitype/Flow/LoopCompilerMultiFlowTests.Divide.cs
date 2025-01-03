using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Flow;

[TestClass]
public class LoopCompilerMultiFlowDivideTests : LoopCompilerMultiFlowTests
{
    [TestMethod]
    public void I_I_Divide()
    {
        // Arrange
        _loopCompiler.Compile("int A1=2*i/(6/3)/2");//1.5
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(1, A1);
    }
    /*
    [TestMethod]
    public void I_IF_Plus()
    {
        _loopCompiler.Compile("int A2=i+(3+2.5)");
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<int>).Value;
        Assert.AreEqual(8, A2);
    }
    [TestMethod]
    public void I_FI_Plus()
    {
        _loopCompiler.Compile("int A3=i+(2.5+3)");
        var A3 = (_flowRepo.Results.Find(x => x.Name == "A3").Value as ValueDo<int>).Value;
        Assert.AreEqual(8, A3);
    }
    */
    [TestMethod]
    public void F_F_Divide()
    {
        _loopCompiler.Compile("float B1=f/(2.0/3.5)/0.5");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
        Assert.AreEqual(7, B1,0.0001);
    }
    /*
    [TestMethod]
    public void F_IF_Plus()
    {
        _loopCompiler.Compile("float B2=f+(2+3.5)+i");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(10.5, B2);
    }
    */
    [TestMethod]
    public void F_IF_Divide_Sub()
    {
        _loopCompiler.Compile("float B2=f/(i/f)/i");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(0.44, B2,0.005);
    }
    [TestMethod]
    public void F_FI_Divide()
    {
        _loopCompiler.Compile("float B2=f/(3.5/2)/i");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(0.38, B2,0.001);
    }
    [TestMethod]
    public void F_FI_Divide_sub()
    {
        _loopCompiler.Compile("float B2=f/(f/i)/i");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
        Assert.AreEqual(1, B2, 0.001);
    }

}
