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
            CreateMap<APIEntities.AccuWeather.Requests.AddToFavoritesRequest, Repository.Entities.Requests.AddToFavoritesRequest>();
            CreateMap<APIEntities.AccuWeather.Requests.DeleteFavoriteRequest, Repository.Entities.Requests.DeleteFavoriteRequest>();

            // Responses
            CreateMap<APIEntities.AccuWeather.Responses.SearchResponse, Repository.Entities.Responses.SearchResponse>();
            CreateMap<APIEntities.AccuWeather.Responses.GetCurrentWeatherResponse, Repository.Entities.Responses.GetCurrentWeatherResponse>();
            CreateMap<APIEntities.AccuWeather.Responses.AddToFavoritesResponse, Repository.Entities.Responses.AddToFavoritesResponse>();
            CreateMap<APIEntities.AccuWeather.Responses.DeleteFavoriteResponse, Repository.Entities.Responses.DeleteFavoriteResponse>();

            // Models
            CreateMap<APIEntities.AccuWeather.Models.PlaceData, Repository.Models.AccuWeatherAPI.AutoComplete.PlaceData>();

            #endregion
        }
    }
}
