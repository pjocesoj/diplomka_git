using MainNode.Logic.Do;

namespace MainNode.Logic.Extentions
{
    public static class ValueExtentions
    {
        public static ValueDo GetValueByname(this ValuesDo values, string name)
        {
            var i=values.Ints.FirstOrDefault(x => x.Name == name);
            if (i != null) { return i; }

            var f=values.Floats.FirstOrDefault(x => x.Name == name);
            if (f != null) { return f; }

            var b =values.Bools.FirstOrDefault(x => x.Name == name);
            if (b != null) { return b; }

            return null;
        }
    }
}
