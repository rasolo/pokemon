using AutoMapper;
using Pokemon.Core.Models;
using System;
using System.Linq.Expressions;

namespace Pokemon.Api.Mapper
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
            CreateMap<PokemonDto, Core.Entities.Pokemon>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url)).ReverseMap();

        }

    }
}
