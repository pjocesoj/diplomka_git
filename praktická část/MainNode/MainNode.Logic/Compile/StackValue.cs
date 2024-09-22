using System.Text;

namespace MainNode.Logic.Compile
{
    internal class StackValue
    {
        public StackValueTypeEnum Type { get; set; }
        public StringBuilder Value { get; set; } = new StringBuilder();
        public object? CachedValue { get; set; }
    }
}
