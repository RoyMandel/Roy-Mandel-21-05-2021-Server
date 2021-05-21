using Framework.Entities.Configurations;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Entities
{
    public class BaseRepository
    {
        public IDbConnection _dbWeather;
        public string _autocompleteUrl;
        public string _currentConditionsUrl;
        public string _apiKey;

        public BaseRepository(IOptions<DBConnectionOptions> dbConnectionOptions, IOptions<AccuWeatherOptions> accuWeatherOptions)
        {
            _dbWeather = new SqlConnection(dbConnectionOptions.Value.WhatherConnectionString);
            _apiKey = accuWeatherOptions.Value.ApiKey;
            _autocompleteUrl = accuWeatherOptions.Value.AutocompleteUrl;
            _currentConditionsUrl = accuWeatherOptions.Value.CurrentConditionsUrl;
        }
    }
}
