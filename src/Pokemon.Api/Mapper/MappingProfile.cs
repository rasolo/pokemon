using AutoMapper;
using Pokemon.Api.Core.Models;

namespace Pokemon.Api.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Pokemon, PokemonDto>()
            .ForMember(dest => dest.moves, opt => opt.MapFrom(src => src.Moves));
            CreateMap<PokemonDto, Core.Entities.Pokemon>()
                .ForMember(dest => dest.Moves, opt => opt.MapFrom(src => src.moves));
            CreateMap<MoveDto, Core.Entities.Move>()
                .ForMember(dest => dest.EffectPercent, opt => opt.MapFrom(src => src.effect_percent));
            CreateMap<EvolutionDto, Core.Entities.Evolution>();
            CreateMap<EvolutionForCreationDto, Core.Entities.Evolution>();
            CreateMap<PokemonForCreationDto, Core.Entities.Pokemon>().ReverseMap();
            CreateMap<PokemonDto, Core.Entities.Pokemon>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url)).ReverseMap();

        }

    }
}
