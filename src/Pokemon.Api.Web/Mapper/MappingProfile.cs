using AutoMapper;
using Pokemon.Api.Web.V1.Models;

namespace Pokemon.Api.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MoveDto, Core.Entities.Move>()
                .ForMember(dest => dest.EffectPercent, opt => opt.MapFrom(src => src.effect_percent)).ReverseMap();
            CreateMap<EvolutionDto, Core.Entities.Evolution>().ReverseMap();
            CreateMap<EvolutionForCreationDto, Core.Entities.Evolution>();
            CreateMap<PokemonForCreationDto, Core.Entities.Pokemon>().ReverseMap();
            CreateMap<PokemonDto, Core.Entities.Pokemon>()
                .ForMember(dest => dest.Moves, opt => opt.MapFrom(src => src.moves))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                .ForMember(dest => dest.Evolutions, opt => opt.MapFrom(src => src.evolutions)).ReverseMap();

        }

    }
}
