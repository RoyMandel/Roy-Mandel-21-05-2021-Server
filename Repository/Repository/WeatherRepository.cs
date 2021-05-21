using Dapper;
using Framework.Entities;
using Framework.Entities.Configurations;
using Framework.Logger;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository.Entities.Interfaces.Repository;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using Repository.Models.DB;
using System;
using System.Collections.Generic;
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
                //_apiKey = "8Yx9iripGXpcJFNlJlEf9fY8Hv8RP9A4";
                //_autocompleteUrl = "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={ApiKey}&q={param}";
                //HttpClient client = new HttpClient();
                //string url = _autocompleteUrl.
                //              Replace("{ApiKey}", _apiKey).
                //              Replace("{param}", searchParam);
                //var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                //string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                string apiResponse = "[{'Version':1,'Key':'212476','Type':'City','Rank':45,'LocalizedName':'Rishon LeZiyyon','Country':{'ID':'IL','LocalizedName':'Israel'},'AdministrativeArea':{'ID':'M','LocalizedName':'Central District'}}]";
                var response = JsonConvert.DeserializeObject<List<Place>>(apiResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("WeatherRepository:AutoCompleteAsync", ex);
                return null;
            }
        }

        public async Task<PlaceTemperature> GetPlaceTemperatureAsync(string CityKey)
        {
            PlaceTemperature response = new PlaceTemperature();
            try
            {
                var placeTemperature = await _dbWeather.QueryFirstOrDefaultAsync<PlaceTemperature>("[usp_GetTemperatureForPlace]", new
                {
                    PlaceID = CityKey
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

        public async Task<PlaceTemperatureAndData> GetCurrentConditionsAsync(string CityKey)
        {
            try
            {
                _apiKey = "8Yx9iripGXpcJFNlJlEf9fY8Hv8RP9A4";
                _autocompleteUrl = "http://dataservice.accuweather.com/currentconditions/v1/{LocationID}?apikey={ApiKey}";
                HttpClient client = new HttpClient();
                string url = _autocompleteUrl.
                              Replace("{ApiKey}", _apiKey).
                              Replace("{LocationID}", CityKey);
                var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                //string apiResponse = "[{'Version':1,'Key':'212476','Type':'City','Rank':45,'LocalizedName':'Rishon LeZiyyon','Country':{'ID':'IL','LocalizedName':'Israel'},'AdministrativeArea':{'ID':'M','LocalizedName':'Central District'}}]";
                var response = JsonConvert.DeserializeObject<List<Place>>(apiResponse);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("WeatherRepository:AutoCompleteAsync", ex);
                return null;
            }
        }
    }
}
