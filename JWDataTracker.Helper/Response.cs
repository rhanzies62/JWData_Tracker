using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Helper
{
    public class Response
    {
        public Response()
        {

        }
        public Response(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public Response(bool isSuccess, string message, dynamic data) : this(isSuccess,message)
        {
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public bool HasException { get; set; }
    }
}
