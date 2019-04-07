using Xunit;

namespace Pokemon.Tests.Api
{
    public class PokemonsControllerShould : PokemonsControllerTestBase
    {
        [Fact]
        protected void ReturnNameDescending()
        {
            ReturnProperty("name", "desc", "Bulbasaur");
        }

        [Fact]
        protected void ReturnNameAscending()
        {
            ReturnProperty("name", "asc", "Caterpie");
        }
    }
}
