

using Pokemon.Api.Core.Paging;

namespace Pokemon.Api.Core.Repositories
{

    public interface IPokemonRepository
    {
        Entities.Pokemon GetByName(string name);
        PagedList<Entities.Pokemon> GetPokemons(PagingParams pagingParams);
        void AddPokemon(Entities.Pokemon pokemon);
    }
}
