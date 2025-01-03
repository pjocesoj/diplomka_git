namespace MainNode.Logic.Test.Compile.Simple.Node;

[TestClass]
public class LoopCompilerNodeCompareTests : LoopCompilerNodeTests
{
    #region <
    [TestMethod]
    public void COMP_L_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a<4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    [TestMethod]
    public void COMP_L_ConstNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4<mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_L_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a<mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    #endregion
    #region >
    [TestMethod]
    public void COMP_G_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a>4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_G_constNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4>mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    [TestMethod]
    public void COMP_G_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a>mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    #endregion
    #region ==
    [TestMethod]
    public void COMP_E_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a==4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_E_ConstNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4==mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_E_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a==mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    #endregion
    #region !=
    [TestMethod]
    public void COMP_NE_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a!=4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    [TestMethod]
    public void COMP_NE_ConstNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4!=mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    [TestMethod]
    public void COMP_NE_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a!=mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    #endregion
    #region <=
    [TestMethod]
    public void COMP_LE_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a<=4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_LE_ConstNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4<=mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_LE_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a<=mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(true, c2);
    }
    #endregion
    #region >=
    [TestMethod]
    public void COMP_GE_NodeConst()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a>=4)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    [TestMethod]
    public void COMP_GE_ConstNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.c=(4>=mock.getValuesG.a)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 5;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(true, c1);
        Assert.AreEqual(false, c2);
    }
    [TestMethod]
    public void COMP_GE_NodeNode()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 4;
        _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a>=mock.getValuesG.b)");
        // Act
        var c0 = _mockNode.Set_c.Value;
        _flowRepo.Run();
        var c1 = _mockNode.Set_c.Value;
        _mockNode.Get_a.Value = 4;
        _flowRepo.Run();
        var c2 = _mockNode.Set_c.Value;
        // Assert
        Assert.AreEqual(false, c1);
        Assert.AreEqual(true, c2);
    }
    #endregion

}
