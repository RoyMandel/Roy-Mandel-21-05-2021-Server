using Framework.API.Entities.Interfaces.Responses;
using Framework.API.Entities.Objects;
using System;
using System.Collections.Generic;

namespace Framework.API.Entities.Responses
{
    public static class ResponseHelper
    {
        public static bool AddErrorData(ref IErrorResponse response, Exception ex)
        {
            return AddErrorData(ref response, ex.Message, ex.Data.ToString(), ex.StackTrace, string.Empty);
        }

        public static bool AddErrorData(ref IErrorResponse response, string message, string data = "", string stackTrace = "", string errorCode = "")
        {
            if (response != null)
            {
                if (response.ErrorList == null)
                    response.ErrorList = new List<ErrorData>();

                response.ErrorList.Add(new ErrorData
                {
                    Data = data,
                    Message = message,
                    StackTrace = stackTrace,
                    ErrorCode = errorCode
                });
                return true;
            }
            else { throw new Exception("[ResponseHelper:AddErrorData] - Response is null"); }
        }
    }
}
