namespace MainNode.Logic.Test.Compile.Simple.Node;

[TestClass]
public class LoopCompilerNodeCompareTests:LoopCompilerNodeTests
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
    #endregion
}
