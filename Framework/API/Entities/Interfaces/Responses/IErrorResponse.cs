using Framework.API.Entities.Objects;
using System.Collections.Generic;

namespace Framework.API.Entities.Interfaces.Responses
{
    public interface IErrorResponse
    {
        List<ErrorData> ErrorList { get; set; }
    }
}
