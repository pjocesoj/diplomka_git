using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Simple.Flow;

[TestClass]
public partial class LoopCompilerFlowIntTests:LoopCompilerFlowTests
{
    #region int + int
    [TestMethod]
    public void III_Plus_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("int A=i+1");
        // Act
        var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<int>).Value;
        // Assert
        Assert.AreEqual(4, A);
    }
    [TestMethod]
    public void III_Plus_ConstFlow()
    {
        _loopCompiler.Compile("int A1=1+i");
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
        Assert.AreEqual(4, A1);
    }
    [TestMethod]
    public void III_Plus_FlowFlow()
    {
        _loopCompiler.Compile("int A2=i+i");
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<int>).Value;
        Assert.AreEqual(6, A2);
    }
    #endregion

    #region (int + int)
    [TestMethod]
    public void III_Plus_Sub_FlowConst()
    {
        _loopCompiler.Compile("int B1=(i+1)");
        var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<int>).Value;
        Assert.AreEqual(4, B1);
    }
    [TestMethod]
    public void III_Plus_Sub_ConstFlow()
    {
        _loopCompiler.Compile("int B2=(1+i)");
        var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<int>).Value;
        Assert.AreEqual(4, B2);
    }
    [TestMethod]
    public void III_Plus_Sub_FlowFlow()
    {
        _loopCompiler.Compile("int B3=(i+i)");
        var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<int>).Value;
        Assert.AreEqual(6, B3);
    }
    [TestMethod]
    public void III_Plus_Sub_ConstConst()
    {
        _loopCompiler.Compile("int B4=(1+1)");
        var B4 = (_flowRepo.Results.Find(x => x.Name == "B4").Value as ValueDo<int>).Value;
        Assert.AreEqual(2, B4);
    }
    #endregion
}
