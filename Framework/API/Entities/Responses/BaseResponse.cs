using Framework.API.Entities.Interfaces.Responses;
using Framework.API.Entities.Objects;
using System;
using System.Collections.Generic;

namespace Framework.API.Entities.Responses
{
    public class BaseResponse : IResponse, IErrorResponse
    {
        public BaseResponse()
        {
            TimeStamp = DateTime.Now;
            ErrorList = new List<ErrorData>();
        }

        public List<ErrorData> ErrorList { get; set; }
        public DateTime TimeStamp { get; set; }
        public Globals.ResponseStatus Status { get; set; }
        public bool IsSuccess { get { return Status == Globals.ResponseStatus.Success; } }
    }
}
