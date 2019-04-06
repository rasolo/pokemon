using Moq;
using Pokemon.Api.Controllers;
using Pokemon.Core.Services;
using Pokemon.Infrastructure.Data;
using Pokemon.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pokemon.Tests.Api
{ 
    public abstract class PokemonsControllerTestBase
    {
        protected readonly IQueryable<Core.Entities.Pokemon> _mockedPokemons;
        protected readonly Mock<IPokemonRepository> _mockedPokemonRepository;
        protected readonly Mock<IPokemonService> _mockedPokemonService;
        protected readonly PagedList<Core.Entities.Pokemon> _pagedListPokemon;
        protected readonly PokemonsController _pokemonsController;

        public PokemonsControllerTestBase()
        {
            _mockedPokemons =
                new List<Core.Entities.Pokemon>
                {
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
                
                    
                        
              

            _mockedPokemonRepository = new Mock<IPokemonRepository>();
            _mockedPokemonService = new Mock<IPokemonService>();
            _pagedListPokemon = new PagedList<Core.Entities.Pokemon>(_mockedPokemons, 1, 5);
            //_pokemonsController = new PokemonsController(_mockedPokemonRepository.Object, _mockedPokemonService.Object);
        }
    }
}