using Framework.API.Entities.Responses;

namespace APIEntities.AccuWeather.Responses
{
    public class AddToFavoritesResponse : BaseResponse
    {
        public string CityKey { get; set; }
        public string CityName { get; set; }
    }
}
