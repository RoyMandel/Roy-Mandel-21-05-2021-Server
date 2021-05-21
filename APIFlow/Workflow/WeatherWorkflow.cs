using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using APIFlow.Entities.Interfaces.DataLayer;
using APIFlow.Entities.Interfaces.Workflow;
using Framework.API.Entities.Responses;
using System;
using System.Threading.Tasks;

namespace APIFlow.Workflow
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
                if (response.IsSuccess)
                {
                    response.Success("SearchAsync");
                }
                else { response.Failed("WeatherWorkflow:SearchAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
