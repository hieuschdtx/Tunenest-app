using System.Net;
using tunenest.Domain.Extensions;

namespace tunenest.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public string code { get; set; }
        public int status { get; set; }

        public BaseException()
        {
        }

        public BaseException(string code, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
        base(message)
        {
            this.code = code;
            this.status = (int)statusCode;
        }

        public virtual object GetMessage() => new { code, message = Message };

        public override string ToString() => GetMessage().ToJson();
    }
}
