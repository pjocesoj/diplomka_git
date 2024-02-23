namespace MainNode.Logic.Do
{
    public class ValuesDo
    {
        public List<ValueDo<int>> Ints { get; set; }=new List<ValueDo<int>>();
        public List<ValueDo<float>> Floats { get; set; } = new List<ValueDo<float>>();
        public List<ValueDo<bool>> Bools { get; set; }=new List<ValueDo<bool>>();
    }
}
