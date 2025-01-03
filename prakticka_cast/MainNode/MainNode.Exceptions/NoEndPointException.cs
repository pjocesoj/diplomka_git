namespace MainNode.Exceptions
{
    public class NoEndPointException : Exception
    {
        public NoEndPointException(string message) : base(message) { }
        public NoEndPointException(string message, Exception inner) : base(message, inner) { }
    }
}
