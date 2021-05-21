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

            // Responses
            CreateMap<APIEntities.AccuWeather.Responses.SearchResponse, Repository.Entities.Responses.SearchResponse>();

            #endregion
        }
    }
}
