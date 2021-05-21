using Framework.API.Entities.Responses;
using Repository.Entities.Interfaces.DataLayer;
using Repository.Entities.Interfaces.Workflow;
using Repository.Entities.Requests;
using Repository.Entities.Responses;
using System;
using System.Threading.Tasks;

namespace Repository.Workflow
{
    public class WeatherWorkflow : IWeatherWorkflow
    {
        IWeatherDataLayer _weatherDataLayer;
        public WeatherWorkflow(IWeatherDataLayer weatherDataLayer)
        {
            _weatherDataLayer = weatherDataLayer;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response)
        {
            try
            {
                response = await _weatherDataLayer.SearchAsync(request, response).ConfigureAwait(false);
                if (!response.IsSuccess)
                {
                    response.Failed("WeatherWorkflow:SearchAsync");
                }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response)
        {
            try
            {
                response = await _weatherDataLayer.GetCurrentWeatherAsync(request, response).ConfigureAwait(false);
                if (!response.IsSuccess)
                {
                    response.Failed("WeatherWorkflow:GetCurrentWeatherAsync");
                }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
