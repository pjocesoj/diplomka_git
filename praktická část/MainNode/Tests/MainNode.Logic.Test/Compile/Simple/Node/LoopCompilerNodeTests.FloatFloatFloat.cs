namespace MainNode.Logic.Test.Compile.Simple.Node
{
    [TestClass]
    public partial class LoopCompilerNodeFloatTests:LoopCompilerNodeTests
    {
        #region plus
        [TestMethod]
        public void FFF_Plus_NodeConst()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b+1.0");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(2.0f, b1);
            Assert.AreEqual(3.0f, b2);
        }
        [TestMethod]
        public void FFF_Plus_NodeNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b+mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(2.0f, b1);
            Assert.AreEqual(4.0f, b2);
        }
        [TestMethod]
        public void FFF_Plus_ConstNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=1.0+mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(2.0f, b1);
            Assert.AreEqual(3.0f, b2);
        }
        #endregion
        #region minus
        [TestMethod]
        public void FFF_Minus_NodeConst()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b-1.0");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(0.0f, b1);
            Assert.AreEqual(1.0f, b2);
        }
        [TestMethod]
        public void FFF_Minus_NodeNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b-mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(0.0f, b1);
            Assert.AreEqual(0.0f, b2);
        }
        [TestMethod]
        public void FFF_Minus_ConstNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=1.0-mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(0.0f, b1);
            Assert.AreEqual(-1.0f, b2);
        }
        #endregion
        #region multiply
        [TestMethod]
        public void FFF_Multiply_NodeConst()
        {
            // Arrange
            _mockNode.Get_b.Value = 2.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b*2.0");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(4.0f, b1);
            Assert.AreEqual(6.0f, b2);
        }
        [TestMethod]
        public void FFF_Multiply_NodeNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 2.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b*mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(4.0f, b1);
            Assert.AreEqual(9.0f, b2);
        }
        [TestMethod]
        public void FFF_Multiply_ConstNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 2.0f;
            _loopCompiler.Compile("mock.setValues.b=2.0*mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(4.0f, b1);
            Assert.AreEqual(6.0f, b2);
        }
        #endregion
        #region divide
        [TestMethod]
        public void FFF_Divide_NodeConst()
        {
            // Arrange
            _mockNode.Get_b.Value = 1.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b/2.0");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(0.5f, b1);
            Assert.AreEqual(1.0f, b2);
        }
        [TestMethod]
        public void FFF_Divide_NodeNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 2.0f;
            _loopCompiler.Compile("mock.setValues.b=mock.getValuesG.b/mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(1.0f, b1);
            Assert.AreEqual(1.0f, b2);
        }
        [TestMethod]
        public void FFF_Divide_ConstNode()
        {
            // Arrange
            _mockNode.Get_b.Value = 2.0f;
            _loopCompiler.Compile("mock.setValues.b=2.0/mock.getValuesG.b");
            // Act
            var b0 = _mockNode.Set_b.Value;
            _flowRepo.Run();
            var b1 = _mockNode.Set_b.Value;
            _mockNode.Get_b.Value=4;
            _flowRepo.Run();
            var b2 = _mockNode.Set_b.Value;
            // Assert
            Assert.AreEqual(1.0f, b1);
            Assert.AreEqual(0.5f, b2);
        }
        #endregion
    }
}
