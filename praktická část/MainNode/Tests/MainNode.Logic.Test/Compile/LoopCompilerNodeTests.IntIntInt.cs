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
            var a0 = _mockNode.Set_a;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a;
            // Assert
            Assert.AreEqual(2, a1.Value);
            Assert.AreEqual(3, a2.Value);
        }
    }
}
