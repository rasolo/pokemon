using AutoMapper;
using Moq;
using Pokemon.Api.Core.Paging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Core.Services;
using System.Collections.Generic;
using System.Linq;
using Pokemon.Api.Web.V1._1._0.Controllers;
using Pokemon.Api.Web.V1._1._0.Models;
using Xunit;

namespace Pokemon.Api.Tests.Api
{
    public abstract class PokemonsControllerTestBase
    {
        protected readonly IQueryable<Core.Entities.Pokemon> MockedPokemons;
        protected readonly Mock<IPokemonRepository> MockedPokemonRepository;
        protected readonly Mock<IPokemonService> MockedPokemonService;
        protected readonly IMapper AutoMapper;
        protected readonly PagedList<Core.Entities.Pokemon> PagedListPokemon;
        protected PokemonsController PokemonsController;

        protected PokemonsControllerTestBase()
        {
            #region Mocked Pokemon
            MockedPokemons =
                new List<Core.Entities.Pokemon>
                {
                     new Core.Entities.Pokemon
                    {
                        Index = 145,
                        Name = "Zapdos",
                        ImageUrl = "http://serebii.net/xy/pokemon/001.png",
                        Types = new List<string>
                        {
                            new string("electric"),
                            new string("flying")
                        }
                     },
                    new Core.Entities.Pokemon
                    {
                        Index = 1,
                        Name = "Bulbasaur",
                        ImageUrl = "http://serebii.net/xy/pokemon/001.png",
                        Types = new List<string>
                        {
                            new string("grass"),
                            new string("poison")
                        },
                        Evolutions =  new List<Core.Entities.Evolution>
                        {
                            new Core.Entities.Evolution
                            {
                                Pokemon = 2,
                                Event = "level-16"
                            }
                        },
                        Moves = new List<Core.Entities.Move>
                        {
                           new Core.Entities.Move
                           {
                               Level = "37",
                               Name = "Seed Bomb",
                               Type = "Grass",
                               Category = "physical",
                               Attack = "80",
                               Accuracy = "100",
                               PP = "15",
                               EffectPercent = "--",
                               Description = "The user slams a barrage of hard-shelled seeds down on the target from above."

                           },
                                new Core.Entities.Move
                           {
                               Level = "3",
                               Name = "Growl",
                               Type = "normal",
                               Category = "other",
                               Attack = "--",
                               Accuracy = "100",
                               PP = "40",
                               EffectPercent = "--",
                               Description = "The user growls in an endearing way, making the opposing team less wary. The foes' Attack stats are lowered."

                           }
                        }
                    },
                     new Core.Entities.Pokemon
                    {
                        Index = 10,
                        Name = "Caterpie",
                        ImageUrl = "http://serebii.net/xy/pokemon/010.png",
                        Types = new List<string>
                        {
                            new string("bug"),
                        },
                        Evolutions =  new List<Core.Entities.Evolution>
                        {
                            new Core.Entities.Evolution
                            {
                                Pokemon = 1,
                                Event = "level-7"
                            }
                        },
                        Moves = new List<Core.Entities.Move>
                        {
                           new Core.Entities.Move
                           {
                               Level = "-",
                               Name = "Tackle",
                               Type = "normal",
                               Category = "physical",
                               Attack = "50",
                               Accuracy = "100",
                               PP = "35",
                               EffectPercent = "--",
                               Description = "A physical attack in which the user charges and slams into the target with its whole body."

                           },
                            new Core.Entities.Move
                           {
                               Level = "-",
                               Name = "String Shot",
                               Type = "bug",
                               Category = "other",
                               Attack = "--",
                               Accuracy = "95",
                               PP = "40",
                               EffectPercent = "--",
                               Description = "The targets are bound with silk blown from the user's mouth. This silk reduces the targets' Speed stat."

                           }
                        }
                    }
                }.AsQueryable();
            #endregion

            MockedPokemonRepository = new Mock<IPokemonRepository>();
            MockedPokemonService = new Mock<IPokemonService>();
            var config = new MapperConfiguration(opts =>
            {
                
                opts.CreateMap<MoveDto, Core.Entities.Move>()
                    .ForMember(dest => dest.EffectPercent, opt => opt.MapFrom(src => src.effect_percent)).ReverseMap();
                opts.CreateMap<EvolutionDto, Core.Entities.Evolution>().ReverseMap();
                opts.CreateMap<EvolutionForCreationDto, Core.Entities.Evolution>();
                opts.CreateMap<PokemonForCreationDto, Core.Entities.Pokemon>().ReverseMap();
                opts.CreateMap<PokemonDto, Core.Entities.Pokemon>()
                    .ForMember(dest => dest.Moves, opt => opt.MapFrom(src => src.moves))
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                    .ForMember(dest => dest.Evolutions, opt => opt.MapFrom(src => src.evolutions)).ReverseMap();
            });

            AutoMapper = config.CreateMapper();
            PagedListPokemon = new PagedList<Core.Entities.Pokemon>(MockedPokemons, 1, 5);
        }

        protected IEnumerable<PokemonDto> GetPokemonsBySortOrder(string propertyToSortOn, string sortOrder, string propertyToOrderBy)
        {
            //Arrange
            var pagingParams = new PagingParams() { Sort = sortOrder };

            MockedPokemonRepository.Setup(x => x.GetPokemons(pagingParams)).Returns(PagedListPokemon);
            MockedPokemonService.Setup(x => x.GetFilteredSortQuery(It.IsAny<string>())).Returns($"{propertyToSortOn} {sortOrder}");
            PokemonsController = new PokemonsController(MockedPokemonRepository.Object, AutoMapper, MockedPokemonService.Object);

            //Act
            GenericApiResponse<ObjectDto> objDto = PokemonsController.GetPokemons(pagingParams);
            return objDto?.Data?.Pokemons;
        }
    }
}