using System.Linq;
using Shouldly;
using Xunit;

namespace Pokemon.Api.Tests.Api
{
    public class PokemonsControllerTests : PokemonsControllerTestBase
    {
        [Fact]
        protected void Give_NameDescending_When_GetPokemons_Then_ReturnNameDescending()
        {
            var pokemons = GetPokemonsBySortOrder("name", "desc", "Zapdos");

            pokemons.First().name.ShouldBe("Zapdos");
        }

        [Fact]
        protected void Give_NameAscending_When_GetPokemons_Then_ReturnNameAscending()
        {
            var pokemons = GetPokemonsBySortOrder("name", "asc", "Bulbasaur");

            pokemons.First().name.ShouldBe("Bulbasaur");
        }
    }
}