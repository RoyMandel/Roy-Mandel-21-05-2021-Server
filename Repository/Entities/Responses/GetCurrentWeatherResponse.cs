using Framework.API.Entities.Responses;

namespace Repository.Entities.Responses
{
    public class GetCurrentWeatherResponse : BaseResponse
    {
        public decimal Temperature { get; set; }
        public string WeatherDescription { get; set; }
    }
}
