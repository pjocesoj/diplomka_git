using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Node
{
    [TestClass]
    public class LoopCompilerMultiNodeBoolTests : LoopCompilerMultiNodeTests
    {
        [TestMethod]
        public void B_B_AndOr()
        {
            // Arrange
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=(false&true)|mock.getValuesG.c");
            // Act
            var a0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_c.Value;
            _mockNode.Get_c.Value = false;
            _flowRepo.Run();
            var a2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, a1);
            Assert.AreEqual(false, a2);
        }
        /*
    [TestMethod]
    public void B_B_AndOrNot()
    {
        _loopCompiler.Compile("bool A2=(!false&true)|b");//ocekava jen 1 znamenko v zavorce
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A2);
    }
    */
        [TestMethod]
        public void B_IB_CompAnd()
        {
            // Arrange
            _mockNode.Get_a.Value = 1;
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.a<2)&mock.getValuesG.c");
            // Act
            var a0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_c.Value;
            _mockNode.Get_a.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, a1);
            Assert.AreEqual(false, a2);
        }
        [TestMethod]
        public void B_FIB_CompAnd()
        {
            // Arrange
            _mockNode.Get_a.Value = 2;
            _mockNode.Get_b.Value = 1;
            _mockNode.Get_c.Value = true;
            _loopCompiler.Compile("mock.setValues.c=(mock.getValuesG.b<mock.getValuesG.a)&mock.getValuesG.c");
            // Act
            var a0 = _mockNode.Set_c.Value;
            _flowRepo.Run();
            var a1 = _mockNode.Set_c.Value;
            _mockNode.Get_b.Value++;
            _flowRepo.Run();
            var a2 = _mockNode.Set_c.Value;
            // Assert
            Assert.AreEqual(true, a1);
            Assert.AreEqual(false, a2);
        }
    }
}
