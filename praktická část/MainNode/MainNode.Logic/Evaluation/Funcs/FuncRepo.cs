using MainNode.Communication.Enums;
using System;

namespace MainNode.Logic.Evaluation.Funcs
{
    public class FuncRepo
    {
        public FuncRepo()
        {
            #region init
            //FunctionsT.Add((typeof(int), typeof(int), "0"), new Func<int, int, int>((a, b) => b));
            //FunctionsT.Add((typeof(float), typeof(float), "0"), new Func<float, float, float>((a, b) => b));
            //FunctionsT.Add((typeof(bool), typeof(bool), "0"), new Func<bool, bool, bool>((a, b) => b));

            //kvuli bugu musim doèasnì prohodit
            FunctionsT.Add((typeof(int), typeof(int), "0"), new Func<int, int, int>((a, b) => a));
            FunctionsT.Add((typeof(float), typeof(float), "0"), new Func<float, float, float>((a, b) => a));
            FunctionsT.Add((typeof(bool), typeof(bool), "0"), new Func<bool, bool, bool>((a, b) => a));
            #endregion

            #region + - * /
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
            #endregion
        }

        public Dictionary<(Type, Type, string), Delegate> FunctionsT = new Dictionary<(Type, Type, string), Delegate>();

        public Delegate GetFunction(Type A, Type B, string op)
        {
            if (FunctionsT.TryGetValue((A, B, op), out var f))
            {
                return f;
            }
            throw new ApplicationException($"Function {A.Name} {op} {B.Name} not found");
        }
    }
}
