using MainNode.Logic.Do;

namespace MainNode.Logic.Test.Compile.Multitype.Const;

[TestClass]
public class LoopCompilerMultiConstBoolTests:LoopCompilerMultiConstTests
{
    [TestMethod]
    public void B_B_AndOr()
    {
        // Arrange
        _loopCompiler.Compile("bool A1=(false&true)|false");
        // Act
        var A1 = (_flowRepo.Results.Find(x => x.Name == "A1").Value as ValueDo<bool>).Value;
        // Assert
        Assert.AreEqual(false, A1);
    }
    /*
    [TestMethod]
    public void B_B_AndOrNot()
    {
        _loopCompiler.Compile("bool A2=(!false&true)|false");//ocekava jen 1 znamenko v zavorce
        var A2 = (_flowRepo.Results.Find(x => x.Name == "A2").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A2);
    }
    */
    [TestMethod]
    public void B_IB_CompAnd()
    {
        _loopCompiler.Compile("bool A3=(1<2)&true");
        var A3 = (_flowRepo.Results.Find(x => x.Name == "A3").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A3);
    }
    [TestMethod]
    public void B_FIB_CompAnd()
    {
        _loopCompiler.Compile("bool A4=(2.5>1)&true");
        var A4 = (_flowRepo.Results.Find(x => x.Name == "A4").Value as ValueDo<bool>).Value;
        Assert.AreEqual(true, A4);
    }
}
