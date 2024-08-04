namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// bool func(float,float)
    /// </summary>
    public static class FuncBoolFloat
    {
        public static bool Greater(float a, float b) { return a > b; }
        public static bool Less(float a, float b) { return a < b; }
        public static bool Equal(float a, float b) { return a==b; }
        public static bool NotEqual(float a, float b) { return a != b; }
    }
}
