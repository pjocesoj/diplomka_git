namespace MainNode.Logic.Do
{
    public class ValueArgDo<T> : ValueDo<T>
    {
        public ValueArgDo(string name, T value, T? min = default, T? max = default, T? def = default) : base(name, value)
        {
            Min = min;
            Max = max;
            Default = def;
        }

        public T? Min { get; set; }
        public T? Max { get; set; }
        public T? Default { get; set; }
    }
}
