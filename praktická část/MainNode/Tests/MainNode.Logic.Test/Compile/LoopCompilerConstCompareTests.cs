using MainNode.Logic.Compile;
using MainNode.Logic.Do;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.Compile
{
    [TestClass]
    public class LoopCompilerConstCompareTests
    {
        private LoopCompiler _loopCompiler;
        private FlowRepository _flowRepo;

        [TestInitialize]
        public void Initialize()
        {
            _flowRepo = new FlowRepository();
            var nodeRepo = new NodeRepository();
            var funcRepo = new FuncRepo();
            _loopCompiler = new LoopCompiler(_flowRepo, nodeRepo, funcRepo);
        }

        [TestMethod]
        public void COMP_L_True()
        {
            // Arrange
            _loopCompiler.Compile("bool L1=(1<2)");
            // Act
            var L1 = (_flowRepo.Results.Find(x => x.Name == "L1").Value as ValueDo<bool>).Value;
            // Assert
            Assert.AreEqual(true, L1);
        }
        [TestMethod]
        public void COMP_L_False()
        {
            // Arrange
            _loopCompiler.Compile("bool L2=(2<1)");
            var L2 = (_flowRepo.Results.Find(x => x.Name == "L2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, L2);
        }

        [TestMethod]
        public void COMP_G_True()
        {
            // Arrange
            _loopCompiler.Compile("bool G1=(2>1)");
            var G1 = (_flowRepo.Results.Find(x => x.Name == "G1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, G1);
        }
        [TestMethod]
        public void COMP_G_False()
        {
            // Arrange
            _loopCompiler.Compile("bool G2=(1>2)");
            var G2 = (_flowRepo.Results.Find(x => x.Name == "G2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, G2);
        }

        [TestMethod]
        public void COMP_E_True()
        {
            // Arrange
            _loopCompiler.Compile("bool E1=(2==2)");
            var E1 = (_flowRepo.Results.Find(x => x.Name == "E1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, E1);
        }
        [TestMethod]
        public void COMP_E_False()
        {
            // Arrange
            _loopCompiler.Compile("bool E2=(1==2)");
            var E2 = (_flowRepo.Results.Find(x => x.Name == "E2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, E2);
        }

        [TestMethod]
        public void COMP_NE_True()
        {
            // Arrange
            _loopCompiler.Compile("bool NE1=(1!=2)");
            var NE1 = (_flowRepo.Results.Find(x => x.Name == "NE1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, NE1);
        }
        [TestMethod]
        public void COMP_NE_False()
        {
            // Arrange
            _loopCompiler.Compile("bool NE2=(2!=2)");
            var NE2 = (_flowRepo.Results.Find(x => x.Name == "NE2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, NE2);
        }

        [TestMethod]
        public void COMP_GE_True()
        {
            // Arrange
            _loopCompiler.Compile("bool GE1=(2>=2)");
            var GE1 = (_flowRepo.Results.Find(x => x.Name == "GE1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, GE1);
        }
        [TestMethod]
        public void COMP_GE_False()
        {
            // Arrange
            _loopCompiler.Compile("bool GE2=(1>=2)");
            var GE2 = (_flowRepo.Results.Find(x => x.Name == "GE2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, GE2);
        }

        [TestMethod]
        public void COMP_LE_True()
        {
            // Arrange
            _loopCompiler.Compile("bool LE1=(2<=2)");
            var LE1 = (_flowRepo.Results.Find(x => x.Name == "LE1").Value as ValueDo<bool>).Value;
            Assert.AreEqual(true, LE1);
        }
        [TestMethod]
        public void COMP_LE_False()
        {
            // Arrange
            _loopCompiler.Compile("bool LE2=(2<=1)");
            var LE2 = (_flowRepo.Results.Find(x => x.Name == "LE2").Value as ValueDo<bool>).Value;
            Assert.AreEqual(false, LE2);
        }
    }
}
