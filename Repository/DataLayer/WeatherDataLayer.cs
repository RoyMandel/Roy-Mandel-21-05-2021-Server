using Framework.API.Entities.Responses;
using Repository.Entities.Interfaces.DataLayer;
using Repository.Entities.Interfaces.Repository;
using Repository.Entities.Requests;
using Repository.Entities.Responses;
using System;
using System.Threading.Tasks;

namespace Repository.DataLayer
{
    public class WeatherDataLayer : IWeatherDataLayer
    {
        IWeatherRepository _weatherRepository;
        public WeatherDataLayer(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response)
        {
            try
            {
                var placesAutoComplete = await _weatherRepository.AutoCompleteAsync(request.SearchParam).ConfigureAwait(false);
                if (placesAutoComplete != null)
                {

                }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
