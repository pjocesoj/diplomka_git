namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// bool func(float,float)
    /// </summary>
    public static class FuncBoolFloat
    {
        public static bool Greater(float a, float b) { return a > b; }
        public static bool Or(bool a, bool b) { return a || b; }
        public static bool Not(bool a, bool b=true) { return !a; }
    }
}
