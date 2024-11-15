using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Flow
{
    [TestClass]
    public class LoopCompilerMultiFlowPLusTests: LoopCompilerMultiFlowTests
    {
        [TestMethod]
        public void I_I_Plus()
        {
            // Arrange
            _loopCompiler.Compile("int A1=i+(2+3)+1");
            // Act
            var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<int>).Value;
            // Assert
            Assert.AreEqual(9, A1);
        }
        /*
        [TestMethod]
        public void I_IF_Plus()
        {
            _loopCompiler.Compile("int A2=i+(3+2.5)");
            var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<int>).Value;
            Assert.AreEqual(8, A2);
        }
        [TestMethod]
        public void I_FI_Plus()
        {
            _loopCompiler.Compile("int A3=i+(2.5+3)");
            var A3 = (_flowRepo.Results.Find(x => x.Name == "A3").Value as ValueDo<int>).Value;
            Assert.AreEqual(8, A3);
        }
        */
        [TestMethod]
        public void F_F_Plus()
        {
            _loopCompiler.Compile("float B1=f+(2.0+3.5)+0.5");
            var B1 = (_flowRepo.Results.Find(x => x.Name == "B1").Value as ValueDo<float>).Value;
            Assert.AreEqual(8, B1);
        }
        /*
        [TestMethod]
        public void F_IF_Plus()
        {
            _loopCompiler.Compile("float B2=f+(2+3.5)+i");
            var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
            Assert.AreEqual(10.5, B2);
        }
        */
        [TestMethod]
        public void F_FI_Plus()
        {
            _loopCompiler.Compile("float B2=f+(3.5+2)+i");
            var B2 = (_flowRepo.Results.Find(x => x.Name == "B2").Value as ValueDo<float>).Value;
            Assert.AreEqual(10.5, B2);
        }
    }
}
