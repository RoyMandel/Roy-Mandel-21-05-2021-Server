using System.ComponentModel.DataAnnotations;

namespace APIEntities.AccuWeather.Requests
{
    public class GetCurrentWeatherRequest
    {
        [Required]
        public string CityKey { get; set; }
        [Required]
        public string CityName { get; set; }
    }
}
