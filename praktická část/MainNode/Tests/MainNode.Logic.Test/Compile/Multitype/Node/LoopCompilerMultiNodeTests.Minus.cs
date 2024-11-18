namespace MainNode.Logic.Test.Compile.Multitype.Node;
[TestClass]
public class LoopCompilerMultiNodeMinusTests : LoopCompilerMultiNodeTests
{
    [TestMethod]
    public void I_I_Minus()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a-(2+3)-1");

        // Act
        var a0 = _mockNode.Set_a.Value;
        _flowRepo.Run();
        var a1 = _mockNode.Set_a.Value;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var a2 = _mockNode.Set_a.Value;
        // Assert
        Assert.AreEqual(-5, a1);
        Assert.AreEqual(-4, a2);
    }
    /*
    public void I_IF_Minus()
    public void I_FI_Minus()
    */
    [TestMethod]
    public void F_F_Minus()
    {
        // Arrange
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b-(2.0+3.5)-0.5");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(-5, b1);
        Assert.AreEqual(-4, b2);
    }
    /*
      public void F_IF_Minus()
     */
    [TestMethod]
    public void F_FI_Minus()
    {
        // Arrange
        _mockNode.Get_a.Value = 1;
        _mockNode.Get_b.Value = 1.0f;
        _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b-(mock.getValuesG.b-mock.getValuesG.a)-1");

        // Act
        var b0 = _mockNode.Set_b.Value;
        _flowRepo.Run();
        var b1 = _mockNode.Set_b.Value;
        _mockNode.Get_b.Value++;
        _mockNode.Get_a.Value++;
        _flowRepo.Run();
        var b2 = _mockNode.Set_b.Value;
        // Assert
        Assert.AreEqual(0, b1);
        Assert.AreEqual(1, b2);
    }

}
