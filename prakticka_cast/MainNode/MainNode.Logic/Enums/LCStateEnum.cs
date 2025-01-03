namespace MainNode.Logic.Enums
{
    internal enum LCStateEnum
    {
        NULL=0,
        NODE,
        ENDPOINT,
        VALUE,
        DOT_EP,
        DOT_VAL,
        OPERATOR,
        UNKNOWN, //nemohu určit jestli je to node, subflow nebo true/false
        FLOW,
        SUBFLOW,
        EQUALS_SIGN
    }
}
