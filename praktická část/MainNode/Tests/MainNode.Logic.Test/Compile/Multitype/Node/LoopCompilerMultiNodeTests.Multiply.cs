namespace MainNode.Logic.Test.Compile.Multitype.Node;
[TestClass]
public class LoopCompilerMultiNodeMultiplyTests : LoopCompilerMultiNodeTests
{
    [TestMethod]
    public void I_I_Multiply()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a*(2*3)*1");

        // Act
        var a0 = _mockNode.Set_a.Value;
        _flowRepo.Run();
        var a1 = _mockNode.Set_a.Value;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var a2 = _mockNode.Set_a.Value;
        // Assert
        Assert.AreEqual(6, a1);
        Assert.AreEqual(12, a2);
    }
    /*
    public void I_IF_Multiply()
    public void I_FI_Multiply()
    */
    [TestMethod]
    public void F_F_Multiply()
    {
        // Arrange
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b*(2.0*3.5)*0.5");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(3.5f, b1);
        Assert.AreEqual(7.0f, b2);
    }
    /*
      public void F_IF_Multiply()
     */
    [TestMethod]
    public void F_FI_Multiply()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b*(mock.getValuesG.b*mock.getValuesG.a)*mock.getValuesG.a");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(1.0f, b1);
        Assert.AreEqual(16.0f, b2);
    }
}
