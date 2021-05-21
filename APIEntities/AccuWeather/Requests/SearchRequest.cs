using System.ComponentModel.DataAnnotations;

namespace APIEntities.AccuWeather.Requests
{
    public class SearchRequest
    {
        [Required]
        public string SearchParam { get; set; }
    }
}
