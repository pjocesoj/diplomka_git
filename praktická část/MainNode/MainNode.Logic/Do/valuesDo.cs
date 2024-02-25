namespace MainNode.Logic.Do
{
    public class ValuesDo
    {
        public List<ValueDo<int>> Ints { get; set; } = new List<ValueDo<int>>();
        public List<ValueDo<float>> Floats { get; set; } = new List<ValueDo<float>>();
        public List<ValueDo<bool>> Bools { get; set; } = new List<ValueDo<bool>>();

        public List<string> ToStringListShort()
        {
            var i = Ints.Select(x => x.ToStringShort());
            var f = Floats.Select(x => x.ToStringShort());
            var b = Bools.Select(x => x.ToStringShort());
            return i.Concat(f).Concat(b).ToList();
        }
    }
}
