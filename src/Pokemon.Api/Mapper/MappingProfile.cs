using AutoMapper;
using System;
using System.Linq.Expressions;

namespace Pokemon.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Pokemon, Models.PokemonDto>()
            .ForMember(dest => dest.moves, opt => opt.MapFrom(src => src.Moves));
            CreateMap<Models.PokemonDto, Core.Entities.Pokemon>()
                .ForMember(dest => dest.Moves, opt => opt.MapFrom(src => src.moves));
            CreateMap<Models.MoveDto, Core.Entities.Move>()
                .ForMember(dest => dest.EffectPercent, opt => opt.MapFrom(src => src.effect_percent));
            CreateMap<Models.EvolutionDto, Core.Entities.Evolution>();

        }

    }
}
