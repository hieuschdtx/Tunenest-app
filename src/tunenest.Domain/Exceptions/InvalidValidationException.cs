using System.Net;

namespace tunenest.Domain.Exceptions
{
    public class InvalidValidationException : BaseException
    {
        public List<InvalidExceptionItemDto> errors { get; }

        public InvalidValidationException(string code, string message) : base(code, message, HttpStatusCode.BadRequest)
        {
        }

        public InvalidValidationException(string code, string message, List<InvalidExceptionItemDto> items) :
            base(code, message, HttpStatusCode.BadRequest)
        {
            if (items is { Count: > 1 }) errors = items;
        }
    }

    public class InvalidExceptionItemDto
    {
        public InvalidExceptionItemDto()
        {
        }

        public InvalidExceptionItemDto(string code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public string message { get; set; }
        public string code { get; set; }
    }
}
