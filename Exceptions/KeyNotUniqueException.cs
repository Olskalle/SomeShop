namespace SomeShop.Exceptions
{
	public class KeyNotUniqueException : Exception
	{
        private static readonly string constMessage = "Only one item expected.";
        public KeyNotUniqueException() : base(constMessage) { }
        public KeyNotUniqueException(string message) 
            : base($"{constMessage} {message}") { }
        public KeyNotUniqueException(string message, Exception inner)
            : base(message, inner) { }
    }
}
