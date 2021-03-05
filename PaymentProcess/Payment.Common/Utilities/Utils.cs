using Payment.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Common.Utilities
{
    public class Utils
    {
        public static ResponseModel<T> GetResponse<T>(bool response, string message, T result) where T : class
        {
            var responseMessage = new ResponseModel<T>();
            responseMessage.IsSuccessResponse = response;
            var responseCode = response ? PaymentEnum.Processed : PaymentEnum.Failed;
            responseMessage.ResponseCode = (int)responseCode;
            responseMessage.Message = message;
            responseMessage.Result = result;
            return responseMessage;
        }
    }
}
