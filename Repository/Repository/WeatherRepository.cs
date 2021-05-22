using Dapper;
using Framework.Entities;
using Framework.Entities.Configurations;
using Framework.Logger;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository.Entities.Interfaces.Repository;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using Repository.Models.AccuWeatherAPI.CurrentConditions;
using Repository.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class WeatherRepository : BaseRepository, IWeatherRepository
    {
        public WeatherRepository(IOptions<DBConnectionOptions> dbSettings, IOptions<AccuWeatherOptions> accuWeatherOptions)
            : base(dbSettings, accuWeatherOptions)
        {

        }

        public async Task<List<Place>> AutoCompleteAsync(string searchParam)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = _autocompleteUrl.
                              Replace("{ApiKey}", _apiKey).
                              Replace("{param}", searchParam);
                var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<List<Place>>(apiResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("WeatherRepository:AutoCompleteAsync", ex);
                return null;
            }
        }

        public async Task<PlaceTemperature> GetPlaceTemperatureAsync(string cityKey)
        {
            PlaceTemperature response = new PlaceTemperature();
            try
            {
                var placeTemperature = await _dbWeather.QueryFirstOrDefaultAsync<PlaceTemperature>("[usp_GetTemperatureForPlace]", new
                {
                    PlaceID = cityKey
                }, commandType: System.Data.CommandType.StoredProcedure);

                if (placeTemperature != null)
                {
                    response = placeTemperature;
                }
                else
                {
                    response = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("[WeatherRepository] => usp_GetTemperatureForPlace failed", ex);
                return null;
            }
        }

        public async Task<List<Conditions>> GetCurrentConditionsAsync(string cityKey)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = _currentConditionsUrl.
                              Replace("{ApiKey}", _apiKey).
                              Replace("{LocationID}", cityKey);
                var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<List<Conditions>>(apiResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("WeatherRepository:GetCurrentConditionsAsync", ex);
                return null;
            }
        }

        public async Task<PlaceTemperatureAndData> InsertPlaceWeatherAndData(string cityKey, string weatherText, decimal temperature, string placeName)
        {
            PlaceTemperatureAndData response = new PlaceTemperatureAndData();
            try
            {
                var placeTemperature = await _dbWeather.QueryFirstOrDefaultAsync<PlaceTemperatureAndData>("[usp_InsertPlaceWeatherAndData]", new
                {
                    PlaceID = cityKey,
                    WeatherText = weatherText,
                    Temperature = temperature,
                    PlaceName = placeName
                }, commandType: System.Data.CommandType.StoredProcedure);

                if (placeTemperature != null)
                {
                    response = placeTemperature;
                }
                else
                {
                    response = null;
                    Logger.Error("[WeatherRepository: InsertPlaceWeatherAndData] => usp_InsertPlaceWeatherAndData failed.");
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("[WeatherRepository:InsertPlaceWeatherAndData] => usp_GetTemperatureForPlace failed", ex);
                return null;
            }
        }

        public async Task<Favorites> AddPlaceToFavorites(string cityKey)
        {
            Favorites response = new Favorites();
            try
            {
                var newFavorite = await _dbWeather.QueryFirstOrDefaultAsync<Favorites>("[usp_AddPlaceToFavorites]", new
                {
                    PlaceID = cityKey
                }, commandType: System.Data.CommandType.StoredProcedure);

                if (newFavorite != null)
                {
                    response.PlaceID = newFavorite.PlaceID;
                    response.PlaceName = newFavorite.PlaceName;
                }
                else
                {
                    response = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("[WeatherRepository] => usp_AddPlaceToFavorites failed", ex);
                return null;
            }
        }

        public async Task<bool> DeleteFromFavorites(string cityKey)
        {
            try
            {
                bool issucces = await _dbWeather.QueryFirstOrDefaultAsync<bool>("[usp_DeleteFavorite]", new
                {
                    PlaceID = cityKey
                }, commandType: System.Data.CommandType.StoredProcedure);

                return issucces;
            }
            catch (Exception ex)
            {
                Logger.Error("[WeatherRepository] => usp_DeleteFavorite failed", ex);
                return false;
            }
        }
    }
}
