using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Payment.Common.Utilities
{
    public class ResponseModel
    {
        [JsonIgnore]
        public bool IsSuccessResponse { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }

    }

    public class Response<T>
    {
        public T Result { get; set; }
    }
}
