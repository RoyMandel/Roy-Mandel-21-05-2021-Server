using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using System.Threading.Tasks;

namespace APIFlow.Entities.Interfaces.Workflow
{
    public interface IWeatherWorkflow
    {
        Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response);
        Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response);
        Task<AddToFavoritesResponse> AddToFavoritesAsync(AddToFavoritesRequest request, AddToFavoritesResponse response);
        Task<DeleteFavoriteResponse> DeleteFavoriteAsync(DeleteFavoriteRequest request, DeleteFavoriteResponse response);
    }
}
