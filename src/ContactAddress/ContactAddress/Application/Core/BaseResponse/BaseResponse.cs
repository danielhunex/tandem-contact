
using System.Collections.Generic;

namespace ContactAddress.Application.Core.Commands
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }
        public BaseResponse(T data) => Data = data;

        public T Data { get; set; }

        public List<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }
    }
}
