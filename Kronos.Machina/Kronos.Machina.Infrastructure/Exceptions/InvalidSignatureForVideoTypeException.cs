namespace Kronos.Machina.Infrastructure.Exceptions
{
    internal class InvalidSignatureForVideoTypeException : Exception
    {
        public InvalidSignatureForVideoTypeException()
        {
        }

        public InvalidSignatureForVideoTypeException(string message)
            : base(message)
        {
        }

        public InvalidSignatureForVideoTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
