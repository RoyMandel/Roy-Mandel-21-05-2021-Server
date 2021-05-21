using Framework.Entities;
using Framework.Entities.Configurations;
using Framework.Logger;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository.Entities.Interfaces.Repository;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using System;
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

        public async Task<AutoCompleteData> AutoCompleteAsync(string searchParam)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = _autocompleteUrl.
                              Replace("{ApiKey}", _apiKey).
                              Replace("{param}", searchParam);
                var serviceResponse = await client.GetAsync(url).ConfigureAwait(false);
                string apiResponse = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<AutoCompleteData>(apiResponse);
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
