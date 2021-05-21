﻿using APIEntities.AccuWeather.Requests;
using APIEntities.AccuWeather.Responses;
using APIFlow.Entities.Interfaces.Workflow;
using Framework.API.Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccuWeatherAPI.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class WeatherController : Controller
    {
        IWeatherWorkflow _weatherWorkflow;
        public WeatherController(IWeatherWorkflow weatherWorkflow)
        {
            _weatherWorkflow = weatherWorkflow;
        }

        [Route("Search")]
        [HttpGet]
        public async Task<SearchResponse> SearchAsync([FromQuery] SearchRequest request)
        {
            var response = new SearchResponse();
            try
            {
                response = await _weatherWorkflow.SearchAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("SearchAsync");
                }
                else
                {
                    response.Failed("WeatherController:SearchAsync");
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
