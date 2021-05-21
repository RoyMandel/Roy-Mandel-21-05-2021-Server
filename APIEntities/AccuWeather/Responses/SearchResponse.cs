using APIEntities.AccuWeather.Models;
using Framework.API.Entities.Responses;
using System.Collections.Generic;

namespace APIEntities.AccuWeather.Responses
{
    public class SearchResponse : BaseResponse
    {
        public List<PlaceData> Places { get; set; }
    }
}
