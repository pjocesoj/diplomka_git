﻿using MainNode.Logic.Do;

namespace MainNode.Logic.Extensions
{
    public static class ValueExtensions
    {
        public static ValueDo GetValueByname(this ValuesDo values, string name)
        {
            var i = values.Ints.FirstOrDefault(x => x.Name == name);
            if (i != null)
            {
                return i;
            }

            var f = values.Floats.FirstOrDefault(x => x.Name == name);
            if (f != null)
            {
                return f;
            }

            var b = values.Bools.FirstOrDefault(x => x.Name == name);
            if (b != null)
            {
                return b;
            }

            return null;
        }

        public static ValueDo DefaultValue(this Type type)
        {
            if (type == typeof(int))
                return new ValueDo<int>("int",0);
            if (type == typeof(float))
                return new ValueDo<float>("float",0.0f);
            if (type == typeof(bool))
                return new ValueDo<bool>("bool",false);

            return null;
        }
    }
}
