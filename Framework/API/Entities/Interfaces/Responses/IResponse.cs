using System;

namespace Framework.API.Entities.Interfaces.Responses
{
    public interface IResponse : IErrorResponse
    {
        DateTime TimeStamp { get; set; }
        Globals.ResponseStatus Status { get; set; }
    }
}
