using System.ComponentModel.DataAnnotations;

namespace APIEntities.AccuWeather.Requests
{
    public class AddToFavoritesRequest
    {
        [Required]
        public string CityKey { get; set; }
    }
}
