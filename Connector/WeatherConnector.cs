using APIEntities.AccuWeather.Models.Interfaces.Connector;
using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using AutoMapper;
using Framework.API.Entities.Responses;
using Repository.Entities.Interfaces.Workflow;
using System;
using System.Threading.Tasks;

namespace Connector
{
    public class WeatherConnector : IWeatherConnector
    {
        private IMapper _mapper;
        private IWeatherWorkflow _weatherWorkflow;
        public WeatherConnector(IMapper mapper, IWeatherWorkflow weatherWorkflow)
        {
            _mapper = mapper;
            _weatherWorkflow = weatherWorkflow;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response)
        {
            try
            {
                // Map request
                var serviceRequest = _mapper.Map<Repository.Entities.Requests.SearchRequest>(request);

                // Map response
                var serviceResponse = _mapper.Map<Repository.Entities.Responses.SearchResponse>(response);

                // Call action
                serviceResponse = await _weatherWorkflow.SearchAsync(serviceRequest, serviceResponse);

                // Merge response logs
                response.Marge(serviceResponse);

                // Handle response
                if (response.IsSuccess)
                {

                }
                else
                {
                    response.Failed("WeatherConnector:SearchAsync");
                }
            }
            catch (Exception ex)
            {
                response.Failed(ex);
            }
            return response;
        }
    }
}
