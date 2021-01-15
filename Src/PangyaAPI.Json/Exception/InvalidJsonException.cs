using System;
namespace PangyaAPI.Json.Exception
{
    /// <summary>
    /// Exception raised when <see cref="JsonParser" /> encounters an invalid token.
    /// </summary>
    public class InvalidJsonException : System.Exception
    {
        public InvalidJsonException(string message)
            : base(message)
        {

        }
    }

}
