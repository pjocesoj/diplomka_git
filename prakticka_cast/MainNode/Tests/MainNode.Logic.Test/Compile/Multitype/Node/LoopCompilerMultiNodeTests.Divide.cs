namespace MainNode.Logic.Test.Compile.Multitype.Node;
[TestClass]
public class LoopCompilerMultiNodeDivideTests : LoopCompilerMultiNodeTests
{
    [TestMethod]
    public void I_I_Divide()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.a=8/(6/3)/mock.getValuesG.a");

        // Act
        var a0 = _mockNode.Set_a.Value;
        _flowRepo.Run();
        var a1 = _mockNode.Set_a.Value;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var a2 = _mockNode.Set_a.Value;
        // Assert
        Assert.AreEqual(4, a1);
        Assert.AreEqual(2, a2);
    }
    /*
    public void I_IF_Divide()
    public void I_FI_Divide()
    */
    [TestMethod]
    public void F_F_Divide()
    {
        // Arrange
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b/(2.0/3.5)/2.5");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(0.7, b1, 0.001);
        Assert.AreEqual(1.4, b2,0.001);
    }
    /*
      public void F_IF_Divide()
     */
    [TestMethod]
    public void F_FI_Divide()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b/(mock.getValuesG.b/2)/mock.getValuesG.a");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(2, b1,0.001);
        Assert.AreEqual(1, b2,0.001);
    }
}
