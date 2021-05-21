﻿using APIFlow.Entities.Interfaces.DataLayer;
using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using System;
using System.Threading.Tasks;
using Framework.API.Entities.Responses;
using APIEntities.AccuWeather.Models.Interfaces.Connector;

namespace APIFlow.DataLayer
{
    public class WeatherDataLayer : IWeatherDataLayer
    {
        IWeatherConnector _weatherConnector;
        public WeatherDataLayer(IWeatherConnector weatherConnector)
        {
            _weatherConnector = weatherConnector;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response)
        {
            try
            {
                response = await _weatherConnector.SearchAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("SearchAsync");
                }
                else { response.Failed("WeatherWorkflow:SearchAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response)
        {
            try
            {
                response = await _weatherConnector.GetCurrentWeatherAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("GetCurrentWeatherAsync");
                }
                else { response.Failed("WeatherWorkflow:GetCurrentWeatherAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
