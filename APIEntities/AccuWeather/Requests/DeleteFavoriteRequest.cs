using System.ComponentModel.DataAnnotations;

namespace APIEntities.AccuWeather.Requests
{
    public class DeleteFavoriteRequest
    {
        [Required]
        public string CityKey { get; set; }
    }
}
