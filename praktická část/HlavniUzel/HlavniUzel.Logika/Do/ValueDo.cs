namespace HlavniUzel.Logika.Do
{
    public class ValueDo<T>
    {
        public string? Name { get; set; } = "val";
        public T Value { get; set; }

        public ValueDo(string name, T value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
