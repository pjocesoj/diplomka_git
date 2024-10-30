using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile;

public partial class LoopCompilerConstTests
{
    [TestMethod]
    public void III_Plus()
    {
        // Arrange
        _loopCompiler.Compile("int A1=1+1");
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(2, A1);
    }
    [TestMethod]
    public void III_Minus()
    {
        _loopCompiler.Compile("int B=1-2");
        var B = (_flowRepo.Results.Find(x => x.Name == "B").Value as ValueDo<int>).Value;
        Assert.AreEqual(-1, B);
    }
    [TestMethod]
    public void III_Multiply()
    {
        _loopCompiler.Compile("int C=2*2");
        var C = (_flowRepo.Results.Find(x => x.Name == "C").Value as ValueDo<int>).Value;
        Assert.AreEqual(4, C);
    }
    [TestMethod]
    public void III_Divide()
    {
        _loopCompiler.Compile("int D=4/2");
        var D = (_flowRepo.Results.Find(x => x.Name == "D").Value as ValueDo<int>).Value;
        Assert.AreEqual(2, D);
    }
}
