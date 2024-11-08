using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Simple.Flow;

[TestClass]
public class LoopCompilerFlowCompareTests : LoopCompilerFlowTests
{
    #region <
    [TestMethod]
    public void COMP_L_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool L1=(i<4)");
        // Act
        var L1 = (_flowRepo.Results.Find(x => x.Name == "L1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, L1);
    }
    [TestMethod]
    public void COMP_L_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool L2=(2<i)");
        // Act
        var L2 = (_flowRepo.Results.Find(x => x.Name == "L2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, L2);
    }
    [TestMethod]
    public void COMP_L_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool L3=(i<i)");
        // Act
        var L3 = (_flowRepo.Results.Find(x => x.Name == "L3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, L3);
    }
    #endregion

    #region >
    [TestMethod]
    public void COMP_G_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool G1=(i>2)");
        // Act
        var G1 = (_flowRepo.Results.Find(x => x.Name == "G1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, G1);
    }
    [TestMethod]
    public void COMP_G_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool G2=(2>i)");
        // Act
        var G2 = (_flowRepo.Results.Find(x => x.Name == "G2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, G2);
    }
    [TestMethod]
    public void COMP_G_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool G3=(i>i)");
        // Act
        var G3 = (_flowRepo.Results.Find(x => x.Name == "G3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, G3);
    }
    #endregion

    #region ==
    [TestMethod]
    public void COMP_E_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool E1=(i==3)");
        // Act
        var E1 = (_flowRepo.Results.Find(x => x.Name == "E1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, E1);
    }
    [TestMethod]
    public void COMP_E_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool E2=(3==i)");
        // Act
        var E2 = (_flowRepo.Results.Find(x => x.Name == "E2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, E2);
    }
    [TestMethod]
    public void COMP_E_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool E3=(i==i)");
        // Act
        var E3 = (_flowRepo.Results.Find(x => x.Name == "E3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, E3);
    }
    #endregion
}
