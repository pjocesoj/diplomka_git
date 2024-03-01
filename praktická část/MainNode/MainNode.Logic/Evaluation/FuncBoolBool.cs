namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// bool func(bool,bool)
    /// </summary>
    public static class FuncBoolBool
    {
        public static bool And(bool a, bool b) { return a && b; }
        public static bool Or(bool a, bool b) { return a || b; }
        public static bool Not(bool a, bool b=true) { return !a; }
    }
}
