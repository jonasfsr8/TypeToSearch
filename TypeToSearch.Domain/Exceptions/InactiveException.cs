namespace TypeToSearch.Domain.Exceptions
{
    public class InactiveException : Exception
    {
        public InactiveException(string message) : base(message) { }

        public InactiveException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
