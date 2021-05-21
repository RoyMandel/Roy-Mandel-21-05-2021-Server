using Framework.API.Entities.Responses;
using Repository.Entities.Interfaces.DataLayer;
using Repository.Entities.Interfaces.Repository;
using Repository.Entities.Requests;
using Repository.Entities.Responses;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DataLayer
{
    public class WeatherDataLayer : IWeatherDataLayer
    {
        IWeatherRepository _weatherRepository;
        public WeatherDataLayer(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, SearchResponse response)
        {
            try
            {
                var placesAutoComplete = await _weatherRepository.AutoCompleteAsync(request.SearchParam).ConfigureAwait(false);
                if (placesAutoComplete?.Count > 0)
                {
                    response.Places = new List<PlaceData>();
                    foreach (var place in placesAutoComplete)
                    {
                        response.Places.Add(new PlaceData
                        {
                            CityKey = place.Key,
                            PlaceName = place.LocalizedName
                        }); 
                    }
                    response.Success("SearchAsync");
                }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GetCurrentWeatherRequest request, GetCurrentWeatherResponse response)
        {
            try
            {
                var placeTemperature = await _weatherRepository.GetPlaceTemperatureAsync(request.CityKey).ConfigureAwait(false);
                if (placeTemperature == null)
                {
                    
                }
                else
                {
                    response.Temperature = placeTemperature.Temperature;
                    response.WeatherDescription = placeTemperature.WeatherText;
                    response.Success("GetCurrentWeatherAsync");
                }
            }
            catch (Exception ex) { response.Failed(ex); }
            return response;
        }
    }
}
