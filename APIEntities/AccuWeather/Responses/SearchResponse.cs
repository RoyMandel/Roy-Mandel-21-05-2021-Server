using Framework.API.Entities.Responses;

namespace APIEntities.AccuWeather.Responses
{
    public class SearchResponse : BaseResponse
    {
        public decimal Temperature { get; set; }
        public string Description { get; set; }
    }
}
