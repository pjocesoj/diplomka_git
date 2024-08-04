namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// int func(float,int)
    /// </summary>
    public static class FuncIntFloat
    {
        public static int Plus(float a, int b) { return (int)a + b; }
        public static int Minus(float a, int b) { return (int)a - b; }
        public static int Multiply(float a, int b) { return (int)a * b; }
        public static int devide(float a, int b) { return (int)a / b; }
    }
}
