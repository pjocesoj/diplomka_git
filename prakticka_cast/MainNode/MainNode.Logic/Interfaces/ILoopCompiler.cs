namespace MainNode.Logic.Interfaces
{
    public interface ILoopCompiler
    {
        void Compile(string input);
        void CompileMultiLine(string input);
    }
}