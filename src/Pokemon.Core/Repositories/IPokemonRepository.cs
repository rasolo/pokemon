using Pokemon.Core.Paging;


namespace Pokemon.Core.Repositories
{

    public interface IPokemonRepository
    {
        Entities.Pokemon GetByName(string name);
        PagedList<Entities.Pokemon> GetPokemons(PagingParams pagingParams);
        void AddPokemon(Entities.Pokemon pokemon);
    }
}
