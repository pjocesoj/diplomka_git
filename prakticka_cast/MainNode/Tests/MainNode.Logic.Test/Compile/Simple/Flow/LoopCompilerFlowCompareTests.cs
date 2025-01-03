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

    #region !=
    [TestMethod]
    public void COMP_NE_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool NE1=(i!=4)");
        // Act
        var NE1 = (_flowRepo.Results.Find(x => x.Name == "NE1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, NE1);
    }
    [TestMethod]
    public void COMP_NE_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool NE2=(4!=i)");
        // Act
        var NE2 = (_flowRepo.Results.Find(x => x.Name == "NE2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, NE2);
    }
    [TestMethod]
    public void COMP_NE_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool NE3=(i!=i)");
        // Act
        var NE3 = (_flowRepo.Results.Find(x => x.Name == "NE3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, NE3);
    }
    #endregion

    #region <=
    [TestMethod]
    public void COMP_LE_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool LE1=(i<=3)");
        // Act
        var LE1 = (_flowRepo.Results.Find(x => x.Name == "LE1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, LE1);
    }
    [TestMethod]
    public void COMP_LE_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool LE2=(2<=i)");
        // Act
        var LE2 = (_flowRepo.Results.Find(x => x.Name == "LE2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, LE2);
    }
    [TestMethod]
    public void COMP_LE_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool LE3=(i<=i)");
        // Act
        var LE3 = (_flowRepo.Results.Find(x => x.Name == "LE3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, LE3);
    }
    #endregion

    #region >=
    [TestMethod]
    public void COMP_GE_FlowConst()
    {
        // Arrange
        _loopCompiler.Compile("bool GE1=(i>=2)");
        // Act
        var GE1 = (_flowRepo.Results.Find(x => x.Name == "GE1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, GE1);
    }
    [TestMethod]
    public void COMP_GE_ConstFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool GE2=(2>=i)");
        // Act
        var GE2 = (_flowRepo.Results.Find(x => x.Name == "GE2").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, GE2);
    }
    [TestMethod]
    public void COMP_GE_FlowFlow()
    {
        // Arrange
        _loopCompiler.Compile("bool GE3=(i>=i)");
        // Act
        var GE3 = (_flowRepo.Results.Find(x => x.Name == "GE3").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(true, GE3);
    }
    #endregion
}
