using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile
{
    public partial class LoopCompilerConstTests
    {
        [TestMethod]
        public void FFF_Plus()
        {
            // Arrange
            _loopCompiler.Compile("float A=1.5+1.5");
            // Act
            var A = (_flowRepo.Results.Find(x => x.Name == "A").Value as ValueDo<float>).Value;
            // Assert
            Assert.AreEqual(3, A);
        }
        [TestMethod]
        public void FFF_Minus()
        {
            _loopCompiler.Compile("float B=1.5-2.5");
            var B = (_flowRepo.Results.Find(x => x.Name == "B").Value as ValueDo<float>).Value;
            Assert.AreEqual(-1, B);
        }
        [TestMethod]
        public void FFF_Multiply()
        {
            _loopCompiler.Compile("float C=1.5*2.5");
            var C = (_flowRepo.Results.Find(x => x.Name == "C").Value as ValueDo<float>).Value;
            Assert.AreEqual(3.75, C);
        }
        [TestMethod]
        public void FFF_Divide()
        {
            _loopCompiler.Compile("float D=1.0/2.0");
            var D = (_flowRepo.Results.Find(x => x.Name == "D").Value as ValueDo<float>).Value;
            Assert.AreEqual(0.5, D);
        }
    }
}
