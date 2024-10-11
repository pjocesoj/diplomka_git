using MainNode.Communication.Enums;

namespace MainNode.Logic.Evaluation.Funcs
{
    public class FuncRepo
    {
        public FuncRepo()
        {
            FunctionsT.Add((typeof(int), typeof(int), "+"), new Func<int, int, int>((a, b) => a + b));
            FunctionsT.Add((typeof(int), typeof(int), "-"), new Func<int, int, int>((a, b) => a - b));
            FunctionsT.Add((typeof(int), typeof(int), "*"), new Func<int, int, int>((a, b) => a * b));
            FunctionsT.Add((typeof(int), typeof(int), "/"), new Func<int, int, int>((a, b) => a / b));

            FunctionsT.Add((typeof(float), typeof(int), "+"), new Func<float, int, float>((a, b) => a + b));
            FunctionsT.Add((typeof(float), typeof(int), "-"), new Func<float, int, float>((a, b) => a - b));
            FunctionsT.Add((typeof(float), typeof(int), "*"), new Func<float, int, float>((a, b) => a * b));
            FunctionsT.Add((typeof(float), typeof(int), "/"), new Func<float, int, float>((a, b) => a / b));

            FunctionsT.Add((typeof(int), typeof(float), "+"), new Func<int, float, float>((a, b) => a + b));
            FunctionsT.Add((typeof(int), typeof(float), "-"), new Func<int, float, float>((a, b) => a - b));
            FunctionsT.Add((typeof(int), typeof(float), "*"), new Func<int, float, float>((a, b) => a * b));
            FunctionsT.Add((typeof(int), typeof(float), "/"), new Func<int, float, float>((a, b) => a / b));

            FunctionsT.Add((typeof(float), typeof(float), "+"), new Func<float, float, float>((a, b) => a + b));
            FunctionsT.Add((typeof(float), typeof(float), "-"), new Func<float, float, float>((a, b) => a - b));
            FunctionsT.Add((typeof(float), typeof(float), "*"), new Func<float, float, float>((a, b) => a * b));
            FunctionsT.Add((typeof(float), typeof(float), "/"), new Func<float, float, float>((a, b) => a / b));

        }

        public Dictionary<(Type, Type, string), Delegate> FunctionsT = new Dictionary<(Type, Type, string), Delegate>();

    }
}
