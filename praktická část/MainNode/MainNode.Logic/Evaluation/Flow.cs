using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class Flow<T>:Flow where T : struct
    {
        public List<Operation<T>> Operations { get; } = new List<Operation<T>>();
        public ValueDo<T>? Output { get; set; }

        public Flow(string name, List<Operation<T>> opers, ValueDo<T> output) : this(name, opers)
        {
            Output = output;
        }
        public Flow(string name, List<Operation<T>> opers)
        {
            Name = name;
            Operations = opers;
        }

        public override void Run()
        {
            Output!.Value = Evaluate();
        }
        public T Evaluate()
        {
            T res = default;
            foreach (var f in Operations)
            {
                T r = f.Execute(res);
                res = r;
            }
            return res;
        }
    }

    public abstract class Flow
    {
        public string Name { get; set; } = "";

        public abstract void Run();
    }
}
