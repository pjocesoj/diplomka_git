using System.Text;

namespace MainNode.Logic.Compile
{
    internal class StackValue
    {
        public StackValueTypeEnum Type { get; set; }
        public StringBuilder Value { get; set; } = new StringBuilder();
        public object? CachedValue { get; set; }

        public override string ToString()
        {
            //return $"{Type}: \"{Value}\" {{{CachedValue}}}";
            var cache = CachedValue != null ? "cache" : "null";
            return $"{Type}: \"{Value}\" ({cache})";
        }
    }
}
