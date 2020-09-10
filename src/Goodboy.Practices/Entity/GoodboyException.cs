using System;
namespace Goodboy.Practices
{
    public class GoodboyException : Exception
    {
        public string Code { get; }

        public GoodboyException()
        {
        }

        public GoodboyException(string code)
        {
            Code = code;
        }

        public GoodboyException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public GoodboyException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public GoodboyException(Exception innerException, string code, string message, params object[] args) : base(string.Format(message, args), innerException)
        {
        }
    }
}
