using AutoMapper;

namespace Connector
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Objects mapping

            // Requests
            CreateMap<APIEntities.AccuWeather.Requests.SearchRequest, Repository.Entities.Requests.SearchRequest>();
            CreateMap<APIEntities.AccuWeather.Requests.GetCurrentWeatherRequest, Repository.Entities.Requests.GetCurrentWeatherRequest>();

            // Responses
            CreateMap<APIEntities.AccuWeather.Responses.SearchResponse, Repository.Entities.Responses.SearchResponse>();
            CreateMap<APIEntities.AccuWeather.Responses.GetCurrentWeatherResponse, Repository.Entities.Responses.GetCurrentWeatherResponse>();

            // Models
            CreateMap<APIEntities.AccuWeather.Models.PlaceData, Repository.Models.AccuWeatherAPI.AutoComplete.PlaceData>();

            #endregion
        }
    }
}
