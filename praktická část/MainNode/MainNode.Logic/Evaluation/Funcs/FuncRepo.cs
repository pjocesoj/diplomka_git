using MainNode.Communication.Enums;

namespace MainNode.Logic.Evaluation.Funcs
{
    public class FuncRepo
    {
        public FuncRepo()
        {
        }

        public Dictionary<(Type, Type, string), Delegate> FunctionsT = new Dictionary<(Type, Type, string), Delegate>();

    }
}
