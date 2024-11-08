namespace MainNode.Logic.Test.Compile.Simple.Node
{
    [TestClass]
    public partial class LoopCompilerNodeBoolTests:LoopCompilerNodeTests
    {
        #region or
        [TestMethod]
        public void BBB_Or_NodeConst()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c|false");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_Or_NodeNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c|mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_Or_ConstNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=false|mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        #endregion

        #region and
        [TestMethod]
        public void BBB_And_NodeConst()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c&true");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_And_NodeNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c&mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_And_ConstNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=true&mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        #endregion

        #region not
        [TestMethod]
        public void BBB_Not_Node()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=!mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(false, c1);
            Assert.AreEqual(true, c2);
        }
        #endregion

        #region or not
        [TestMethod]
        public void BBB_OrNot_NodeConst() 
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c|!true");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_OrNot_NodeNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c|!mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(true, c2);
        }
        [TestMethod]
        public void BBB_OrNot_ConstNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=false|!mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(false, c1);
            Assert.AreEqual(true, c2);
        }
        #endregion

        #region and not
        [TestMethod]
        public void BBB_AndNot_NodeConst()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c&!false");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_AndNot_NodeNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=mock.getValuesG.c&!mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(false, c1);
            Assert.AreEqual(false, c2);
        }
        [TestMethod]
        public void BBB_AndNot_ConstNode()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=true&!mock.getValuesG.c");
            // Act
            var c0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var c1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var c2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(false, c1);
            Assert.AreEqual(true, c2);
        }
        #endregion
    }
}
