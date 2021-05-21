using Repository.Entities.Requests;
using Repository.Entities.Responses;
using System.Threading.Tasks;

namespace Repository.Entities.Interfaces.DataLayer
{
    public interface IWeatherDataLayer
    {
        Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response);
        Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response);
    }
}
