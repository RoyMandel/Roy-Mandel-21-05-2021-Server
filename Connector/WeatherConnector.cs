using APIEntities.AccuWeather.Models.Interfaces.Connector;
using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using AutoMapper;
using Framework.API.Entities.Responses;
using Repository.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
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
                    response.Places = new List<APIEntities.AccuWeather.Models.PlaceData>();
                    foreach (var place in serviceResponse.Places)
                    {
                        response.Places.Add(new APIEntities.AccuWeather.Models.PlaceData
                        {
                            CityKey = place.CityKey,
                            PlaceName = place.PlaceName
                        });
                    }
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

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response)
        {
            try
            {
                // Map request
                var serviceRequest = _mapper.Map<Repository.Entities.Requests.GetCurrentWeatherRequest>(request);

                // Map response
                var serviceResponse = _mapper.Map<Repository.Entities.Responses.GetCurrentWeatherResponse>(response);

                // Call action
                serviceResponse = await _weatherWorkflow.GetCurrentWeatherAsync(serviceRequest, serviceResponse);

                // Merge response logs
                response.Marge(serviceResponse);

                // Handle response
                if (response.IsSuccess)
                {
                    response.Temperature = serviceResponse.Temperature;
                    response.WeatherDescription = serviceResponse.WeatherDescription;
                }
                else
                {
                    response.Failed("WeatherConnector:GetCurrentWeatherAsync");
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
