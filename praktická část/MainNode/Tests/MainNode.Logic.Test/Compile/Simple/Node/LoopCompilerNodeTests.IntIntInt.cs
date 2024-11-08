namespace MainNode.Logic.Test.Compile.Simple.Node
{
    [TestClass]
    public partial class LoopCompilerNodeIntTests:LoopCompilerNodeTests
    {
        #region plus
        [TestMethod]
        public void III_Plus_NodeConst()
        {
            // Arrange
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
        [TestMethod]
        public void III_Plus_NodeNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a+mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(2, a1);
            Assert.AreEqual(4, a2);
        }
        [TestMethod]
        public void III_Plus_ConstNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=1+mock.getValuesG.a");

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
        #endregion
        #region minus
        [TestMethod]
        public void III_Minus_NodeConst()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a-1");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(0, a1);
            Assert.AreEqual(1, a2);
        }
        [TestMethod]
        public void III_Minus_NodeNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a-mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(0, a1);
            Assert.AreEqual(0, a2);
        }
        [TestMethod]
        public void III_Minus_ConstNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _loopCompiler.Compile("mock.setValues.a=1-mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(0, a1);
            Assert.AreEqual(-1, a2);
        }
        #endregion
        #region multiply
        [TestMethod]
        public void III_Multiply_NodeConst()
        {
            // Arrange
            _mockNode.Get_a.Value = 2;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a*2");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(4, a1);
            Assert.AreEqual(6, a2);
        }
        [TestMethod]
        public void III_Multiply_NodeNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 2;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a*mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(4, a1);
            Assert.AreEqual(9, a2);
        }
        [TestMethod]
        public void III_Multiply_ConstNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 2;
            _loopCompiler.Compile("mock.setValues.a=2*mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(4, a1);
            Assert.AreEqual(6, a2);
        }
        #endregion
        #region divide
        [TestMethod]
        public void III_Divide_NodeConst()
        {
            // Arrange
            _mockNode.Get_a.Value = 4;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a/2");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value = 2;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(2, a1);
            Assert.AreEqual(1, a2);
        }
        [TestMethod]
        public void III_Divide_NodeNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 4;
            _loopCompiler.Compile("mock.setValues.a=mock.getValuesG.a/mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(1, a1);
            Assert.AreEqual(1, a2);
        }
        [TestMethod]
        public void III_Divide_ConstNode()
        {
            // Arrange
            _mockNode.Get_a.Value = 4;
            _loopCompiler.Compile("mock.setValues.a=4/mock.getValuesG.a");

            // Act
            var a0 = _mockNode.Set_a.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_a.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_a.Value;
            // Assert
            Assert.AreEqual(1, a1);
            Assert.AreEqual(0, a2);
        }
        #endregion
    }
}
