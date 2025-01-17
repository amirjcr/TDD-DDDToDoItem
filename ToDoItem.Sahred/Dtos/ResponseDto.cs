using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Sahred.Dtos
{
    public class ResponseDto
    {
        public bool IsSucced { get; protected set; }
        public string? Message { get; protected set; }
        public Exception? Exception { get; protected set; }

        public ResponseDto OnSuccess(string message)
        {
            this.IsSucced = true;
            this.Message = message;
            return this;
        }

        public ResponseDto OnFailure(string message)
        {
            this.IsSucced = false;
            this.Message = message;
            return this;
        }

        public virtual ResponseDto OnException(string message, Exception? ex)
        {
            this.IsSucced = false;
            this.Message = message;
            this.Exception = ex;
            return this;
        }

    }


    public class ResponseDto<T> : ResponseDto
    {
        public T? Data { get; private set; }

        public ResponseDto<T> OnSuccess(string message, T data)
        {
            this.IsSucced = true;
            this.Message = message;
            this.Data = data;
            return this;
        }
        public ResponseDto<T> OnFailer(string message, T data)
        {
            this.IsSucced = false;
            this.Message = message;
            this.Data = data;
            return this;
        }
        public ResponseDto<T> OnException(string message, T data, Exception? exception)
        {
            this.IsSucced = false;
            this.Message = message;
            this.Data = data;
            this.Exception = exception;
            return this;
        }

        public override ResponseDto<T> OnException(string message, Exception? exception)
        {
            this.IsSucced = false;
            this.Message = message;
            this.Exception = exception;
            return this;
        }
    }
}
