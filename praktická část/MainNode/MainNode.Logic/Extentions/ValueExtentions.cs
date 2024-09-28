using MainNode.Logic.Do;

namespace MainNode.Logic.Extentions
{
    public static class ValueExtentions
    {
        public static ValueDo GetValueByname(this ValuesDo values, string name, out Type T)
        {
            var i = values.Ints.FirstOrDefault(x => x.Name == name);
            if (i != null)
            {
                T = typeof(int);
                return i;
            }

            var f = values.Floats.FirstOrDefault(x => x.Name == name);
            if (f != null)
            {
                T = typeof(float);
                return f;
            }

            var b = values.Bools.FirstOrDefault(x => x.Name == name);
            if (b != null)
            {
                T = typeof(bool);
                return b;
            }

            T = null;
            return null;
        }

    }
}
