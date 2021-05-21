using Framework.API.Entities.Responses;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using System.Collections.Generic;

namespace Repository.Entities.Responses
{
    public class SearchResponse : BaseResponse
    {
        public List<PlaceData> Places { get; set; }
    }
}
