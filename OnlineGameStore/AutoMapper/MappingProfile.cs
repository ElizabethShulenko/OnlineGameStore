using AutoMapper;
using OnlineGameStore.API.Models.Request;
using OnlineGameStore.Core.Models;

namespace OnlineGameStore.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameRequest, GameModel>();

            CreateMap<GenreRequest, GenreModel>();
        }
    }
}
