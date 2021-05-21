using Framework.API.Entities.Interfaces.Responses;
using Framework.API.Entities.Objects;
using System;
using System.Collections.Generic;

namespace Framework.API.Entities.Responses
{
    public static class ResponseExtensionMethods
    {
        public static bool Failed(this IResponse response, Exception ex)
        {
            response.ErrorList = response.ErrorList ?? new List<ErrorData>();
            response.Status = Globals.ResponseStatus.Fail;
            return response.AddErrorData(ex);
        }

        public static bool Failed(this IResponse response, string message, string data = "", string stackTrack = "", string errorCode = "")
        {
            response.ErrorList = response.ErrorList ?? new List<ErrorData>();
            response.Status = Globals.ResponseStatus.Fail;
            response.AddErrorData(message, data, stackTrack, errorCode);
            return true;
        }

        public static IResponse Success(this IResponse response, string message, string data = "")
        {
            if (response == null)
                response = new BaseResponse();
            if (response.ErrorList == null || response.ErrorList.Count == 0)
            {
                response.Status = Globals.ResponseStatus.Success;
                return response;
            }
            else
            { throw new Exception("[ExtensionMethods:Success] - Unable to set response status as Success. ErrorList is not null"); }
        }

        public static IResponse Marge(this IResponse response, IResponse responseForMarge)
        {
            if (responseForMarge == null) { throw new Exception("ResponseExtensionMethods => Seccess => responseForMarge is not empty"); }
            if (response == null) { response = new BaseResponse(); }

            response.Status = responseForMarge.Status;
            if (responseForMarge.ErrorList != null)
            {
                response.ErrorList = response.ErrorList ?? new List<ErrorData>();
                response.ErrorList.AddRange(responseForMarge.ErrorList);
            }

            return response;
        }

        public static bool AddErrorData(this IErrorResponse response, Exception ex)
        {
            return ResponseHelper.AddErrorData(ref response, ex);
        }

        public static bool AddErrorData(this IErrorResponse response, string message, string data = "", string stackTrack = "", string errorCode = "")
        {
            return ResponseHelper.AddErrorData(ref response, message, data, stackTrack, errorCode);
        }
    }
}
