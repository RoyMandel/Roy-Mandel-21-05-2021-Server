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

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response)
        {
            try
            {
                response = await _weatherDataLayer.GetCurrentWeatherAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("GetCurrentWeatherAsync");
                }
                else { response.Failed("WeatherWorkflow:GetCurrentWeatherAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }

        public async Task<AddToFavoritesResponse> AddToFavoritesAsync(AddToFavoritesRequest request, AddToFavoritesResponse response)
        {
            try
            {
                response = await _weatherDataLayer.AddToFavoritesAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("AddToFavoritesAsync");
                }
                else { response.Failed("WeatherWorkflow:AddToFavoritesAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }

        public async Task<DeleteFavoriteResponse> DeleteFavoriteAsync(DeleteFavoriteRequest request, DeleteFavoriteResponse response)
        {
            try
            {
                response = await _weatherDataLayer.DeleteFavoriteAsync(request, response).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    response.Success("DeleteFavoriteAsync");
                }
                else { response.Failed("WeatherWorkflow:DeleteFavoriteAsync"); }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
