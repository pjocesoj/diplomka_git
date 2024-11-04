namespace MainNode.Logic.Test.Compile
{
    public partial class LoopCompilerNodeTests
    {
        [TestMethod]
        public void III_Plus_NodeConst()
        {
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a+1");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(2, a1);
            Assert.AreEqual(3, a2);
        }
    }
}
