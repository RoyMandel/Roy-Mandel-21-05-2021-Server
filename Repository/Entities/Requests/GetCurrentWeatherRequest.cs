namespace Repository.Entities.Requests
{
    public class GetCurrentWeatherRequest
    {
        public string CityKey { get; set; }
        public string CityName { get; set; }
    }
}
