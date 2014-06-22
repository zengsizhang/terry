namespace TimeSearcher
{
    using System;

    public class FileFormatException : ApplicationException
    {
        public FileFormatException()
        {
        }

        public FileFormatException(string message) : base(message)
        {
        }
    }
}

