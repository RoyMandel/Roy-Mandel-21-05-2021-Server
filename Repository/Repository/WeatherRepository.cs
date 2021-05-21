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

        public async Task<PlaceTemperature> GetPlaceTemperatureAsync(string cityKey)
        {
            PlaceTemperature response = new PlaceTemperature();
            try
            {
                IDbConnection _dbWeather = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=RealcommerceDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
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
                //_apiKey = "8Yx9iripGXpcJFNlJlEf9fY8Hv8RP9A4";
                //_currentConditionsUrl = "http://dataservice.accuweather.com/currentconditions/v1/{LocationID}?apikey={ApiKey}";
                //HttpClient client = new HttpClient();
                //string url = _currentConditionsUrl.
                //              Replace("{ApiKey}", _apiKey).
                //              Replace("{LocationID}", cityKey);
                //var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                //string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                string apiResponse = @"[{'LocalObservationDateTime':'2021-05-21T22:26:00+03:00','EpochTime':1621625160,'WeatherText':'Mostly clear','WeatherIcon':34,'HasPrecipitation':false,'PrecipitationType':null,'IsDayTime':false,'Temperature':{'Metric':{'Value':22.6,'Unit':'C','UnitType':17},'Imperial':{'Value':73.0,'Unit':'F','UnitType':18}},'MobileLink':'http://m.accuweather.com/en/il/rishon-leziyyon/212476/current-weather/212476?lang=en-us','Link':'http://www.accuweather.com/en/il/rishon-leziyyon/212476/current-weather/212476?lang=en-us'}]";
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
                IDbConnection _dbWeather = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=RealcommerceDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
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
    }
}
