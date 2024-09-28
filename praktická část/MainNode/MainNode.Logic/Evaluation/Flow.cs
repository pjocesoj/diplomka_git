using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class Flow<T>:Flow where T : struct
    {
        public List<Operation<T>> Operations { get; } = new List<Operation<T>>();
        public ValueDo<T>? Output { get; set; }

        public Flow(string name, List<Operation<T>> opers)
        {
            Name = name;
            Operations = opers;
        }
        public Flow(string name)
        {
            Name = name;
            Operations = new List<Operation<T>>();
        }

        public override void Run()
        {
            var res = Evaluate();
            if(Output != null) {  Output.Value = res; }
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

        public override FlowResult GetResult()
        {
            return new FlowResult<T>(this);
        }
    }

    public abstract class Flow
    {
        public string Name { get; set; } = "";

        public abstract void Run();

        public abstract FlowResult GetResult();

        public static Flow Create(Type type, string name)
        {
            if (type == typeof(int))
                return new Flow<int>(name);
            if (type == typeof(float))
                return new Flow<float>(name);
            if (type == typeof(bool))
                return new Flow<bool>(name);

            return null;
        }
    }
}
