namespace tunenest.Domain.Helpers
{
    public class BaseResult
    {
        public BaseResult()
        {
        }

        public BaseResult(bool succeeded, string messages)
        {
            this.succeeded = succeeded;
            this.messages = messages;
        }

        public bool succeeded { get; set; }
        public string messages { get; set; }
    }

    public class CreateSuccessResult<T> : BaseResult
    {
        public CreateSuccessResult() : base(true, "")
        {
        }

        public CreateSuccessResult(string messages) : base(true, messages)
        {
        }

        public CreateSuccessResult(T data, string messages) : base(true, messages)
        {
            this.data = data;
        }

        public CreateSuccessResult(T data)
        {
            this.succeeded = true;
            this.data = data;
        }

        public T data { get; set; }
    }

    public class CreateErrorResult<T> : BaseResult
    {
        public CreateErrorResult() : base(false, "")
        {
        }

        public CreateErrorResult(string messages) : base(false, messages)
        {
        }

        public CreateErrorResult(int statusCode, string messages, T errors) : base(false, messages)
        {
            this.status_code = statusCode;
            this.errors = errors;
        }

        public T errors { get; set; }
        public int status_code { get; set; }
    }
}
