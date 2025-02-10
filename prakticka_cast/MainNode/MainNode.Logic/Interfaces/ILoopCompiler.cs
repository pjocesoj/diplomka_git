namespace MainNode.Logic.Interfaces
{
    public interface ILoopCompiler
    {
        void Compile(string input);
        void CompileMultyLine(string input);
    }
}