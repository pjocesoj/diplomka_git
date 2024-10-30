using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile
{
    public partial class LoopCompilerFlowTests
    {
        #region float + float
        [TestMethod]
        public void FFF_Plus_FlowConst()
        {
            // Arrange
            _loopCompiler.Compile("float A=f+1.0");
            // Act
            var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<float>).Value;
            // Assert
            Assert.AreEqual(3, A);
        }
        [TestMethod]
        public void FFF_Plus_ConstFlow()
        {
            _loopCompiler.Compile("float A1=1.0+f");
            var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<float>).Value;
            Assert.AreEqual(3, A1);
        }
        [TestMethod]
        public void FFF_Plus_FlowFlow()
        {
            _loopCompiler.Compile("float A2=f+f");
            var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<float>).Value;
            Assert.AreEqual(4, A2);
        }
        [TestMethod]
        public void FFF_Plus_ConstConst()
        {
            _loopCompiler.Compile("float A3=1.0+1.0");
            var A3 = (_flowRepo.Results.Find(x => x.Name == "A3").Value as ValueDo<float>).Value;
            Assert.AreEqual(2, A3);
        }
        #endregion
    
        #region (float + float)
        [TestMethod]
        public void FFF_Plus_Sub_FlowConst()
        {
            _loopCompiler.Compile("float B1=(f+1.0)");
            var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
            Assert.AreEqual(3, B1);
        }
        [TestMethod]
        public void FFF_Plus_Sub_ConstFlow()
        {
            _loopCompiler.Compile("float B2=(1.0+f)");
            var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
            Assert.AreEqual(3, B2);
        }
        [TestMethod]
        public void FFF_Plus_Sub_FlowFlow()
        {
            _loopCompiler.Compile("float B3=(f+f)");
            var B3 = (_flowRepo.Results.Find(x => x.Name == "B3").Value as ValueDo<float>).Value;
            Assert.AreEqual(4, B3);
        }
        [TestMethod]
        public void FFF_Plus_Sub_ConstConst()
        {
            _loopCompiler.Compile("float B4=(1.0+1.0)");
            var B4 = (_flowRepo.Results.Find(x => x.Name == "B4").Value as ValueDo<float>).Value;
            Assert.AreEqual(2, B4);
        }
        #endregion
    }
}
