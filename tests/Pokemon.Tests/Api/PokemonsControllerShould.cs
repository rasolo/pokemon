using Xunit;

namespace Pokemon.Api.Tests.Api
{
    public class PokemonsControllerShould : PokemonsControllerTestBase
    {
        [Fact]
        protected void ReturnNameDescending()
        {
            ReturnProperty("name", "desc", "Caterpie");
        }

        [Fact]
        protected void ReturnNameAscending()
        {
            ReturnProperty("name", "asc", "Bulbasaur");
        }
    }
}
