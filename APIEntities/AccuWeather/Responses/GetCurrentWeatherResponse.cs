using Framework.API.Entities.Responses;

namespace APIEntities.AccuWeather.Responses
{
    public class GetCurrentWeatherResponse : BaseResponse
    {
        public decimal Temperature { get; set; }
        public string WeatherDescription { get; set; }
    }
}
