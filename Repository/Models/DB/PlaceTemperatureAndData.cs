namespace Repository.Models.DB
{
    public class PlaceTemperatureAndData
    {
        public string PlaceID { get; set; }
        public string PlaceName { get; set; }
        public decimal Temperature { get; set; }
        public string WeatherText { get; set; }
    }
}
