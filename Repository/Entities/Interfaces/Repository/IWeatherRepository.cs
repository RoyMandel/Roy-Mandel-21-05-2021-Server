using Repository.Models.AccuWeatherAPI.AutoComplete;
using Repository.Models.AccuWeatherAPI.CurrentConditions;
using Repository.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Entities.Interfaces.Repository
{
    public interface IWeatherRepository
    {
        Task<List<Place>> AutoCompleteAsync(string searchParam);
        Task<PlaceTemperature> GetPlaceTemperatureAsync(string CityKey);
        Task<List<Conditions>> GetCurrentConditionsAsync(string CityKey);
        Task<PlaceTemperatureAndData> InsertPlaceWeatherAndData(string cityKey, string weatherText, decimal temperature, string placeName);
    }
}
