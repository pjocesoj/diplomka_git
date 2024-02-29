using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class Flow<T>
    {
        public string Name { get; set; }
        public List<Operation<T>> Operations { get; } = new List<Operation<T>>();
        public ValueDo<T> Output { get; set; }
        public Flow(string namme, List<Operation<T>> opers, ValueDo<T> output)
        {
            Name = namme;
            Operations = opers;
            Output= output;
        }

        public void Run()
        {
            T res = default;
            foreach (var f in Operations)
            {
                T r = f.Execute(res);
                res = r;
            }
            Output.Value = res;
        }
    }
}
