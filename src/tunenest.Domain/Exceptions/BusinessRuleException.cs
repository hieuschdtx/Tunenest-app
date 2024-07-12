using System.Net;

namespace tunenest.Domain.Exceptions
{
    public class BusinessRuleException : BaseException
    {
        public BusinessRuleException(string code, string message) : base(code, message)
        {
        }

        public BusinessRuleException(string code, string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
            base(code, message, statusCode)
        {
        }

        public override dynamic GetMessage()
        {
            return new { code, message = Message };
        }
    }
}
