using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile
{
    public partial class LoopCompilerFlowTests
    {
        #region bool + bool
        [TestMethod]
        public void BBB_Plus_FlowConst()
        {
            // Arrange
            _loopCompiler.Compile("bool A=b+true");
            // Act
            var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<bool>).Value;
            // Assert
            Assert.AreEqual(true, A);
        }
        [TestMethod]
        public void BBB_Plus_ConstFlow()
        {
            // Arrange
            _loopCompiler.Compile("bool A1=true+b");
            // Act
            var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<bool>).Value;
            // Assert
            Assert.AreEqual(true, A1);
        }
        [TestMethod]
        public void BBB_Plus_FlowFlow()
        {
            // Arrange
            _loopCompiler.Compile("bool A2=b+b");
            // Act
            var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<bool>).Value;
            // Assert
            Assert.AreEqual(true, A2);
        }
        #endregion

        #region (bool + bool)
        [TestMethod]
        public void BBB_Plus_Sub_FlowConst()
        {
            _loopCompiler.Compile("bool B1=(b+true)");
            var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, B1);
        }
        [TestMethod]
        public void BBB_Plus_Sub_ConstFlow()
        {
            _loopCompiler.Compile("bool B2=(true+b)");
            var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, B2);
        }
        [TestMethod]
        public void BBB_Plus_Sub_FlowFlow()
        {
            _loopCompiler.Compile("bool B3=(b+b)");
            var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, B3);
        }
        [TestMethod]
        public void BBB_Plus_Sub_ConstConst()
        {
            _loopCompiler.Compile("bool B4=(true+true)");
            var B4 = (_flowRepo.Results.Find(x => x.Name == "B4").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, B4);
        }

        #endregion
    }
}
