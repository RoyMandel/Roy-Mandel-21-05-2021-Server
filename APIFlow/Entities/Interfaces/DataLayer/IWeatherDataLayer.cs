using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using System.Threading.Tasks;

namespace APIFlow.Entities.Interfaces.DataLayer
{
    public interface IWeatherDataLayer
    {
        Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response);
        Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response);
    }
}
