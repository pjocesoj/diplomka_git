﻿namespace MainNode.Logic.Do
{
    public class ValueDo<T>:ValueDo
    {
        public T Value { get; set; }

        public ValueDo(string name, T value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            Type t = typeof(T);
            return $"{t.Name} {Name} = {Value}";
        }
        public string ToStringShort()
        {
            if (Value is int) { return $"int {Name}"; }//Int32
            if (Value is float) { return $"float {Name}"; }//Single
            if (Value is bool) { return $"bool {Name}"; }//Boolean

            return $"{typeof(T).Name} {Name}";
        }

        public override Type GetT()
        {
            return typeof(T);
        }
    }

    public abstract class ValueDo 
    {
        public string? Name { get; set; } = "val";

        public abstract Type GetT();
    }
}
