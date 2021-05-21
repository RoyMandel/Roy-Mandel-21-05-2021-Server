using APIEntities.AccuWeather.Requests;
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

        [Route("GetCurrentWeather")]
        [HttpGet]
        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync([FromQuery] GetCurrentWeatherRequest request)
        {
            var response = new GetCurrentWeatherResponse();
            try
            {
                response = await _weatherWorkflow.GetCurrentWeatherAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("GetCurrentWeatherAsync");
                }
                else
                {
                    response.Failed("WeatherController:GetCurrentWeatherAsync");
                }
            }
            catch (Exception ex)
            {
                response.Failed(ex);
            }
            return response;
        }

        [Route("AddToFavorites")]
        [HttpPost]
        public async Task<AddToFavoritesResponse> AddToFavoritesAsync([FromBody] AddToFavoritesRequest request)
        {
            var response = new AddToFavoritesResponse();
            try
            {
                response = await _weatherWorkflow.AddToFavoritesAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("AddToFavoritesAsync");
                }
                else
                {
                    response.Failed("WeatherController:AddToFavoritesAsync");
                }
            }
            catch (Exception ex)
            {
                response.Failed(ex);
            }
            return response;
        }

        [Route("DeleteFavorite")]
        [HttpPost]
        public async Task<DeleteFavoriteResponse> DeleteFavoriteAsync([FromBody] DeleteFavoriteRequest request)
        {
            var response = new DeleteFavoriteResponse();
            try
            {
                response = await _weatherWorkflow.DeleteFavoriteAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("DeleteFavoriteAsync");
                }
                else
                {
                    response.Failed("WeatherController:DeleteFavoriteAsync");
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
