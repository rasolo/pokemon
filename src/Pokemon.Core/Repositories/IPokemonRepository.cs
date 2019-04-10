

using Pokemon.Api.Core.Paging;

namespace Pokemon.Api.Core.Repositories
{

    public interface IPokemonRepository
    {
        Entities.Pokemon GetByName(string name);
        PagedList<Entities.Pokemon> GetPokemons(PagingParams pagingParams);
        void AddPokemon(Entities.Pokemon pokemon);
        /// <summary>Deletes a Pokemon.
        /// <param name="name">The name of the Pokemon to delete.</param>
        /// <returns>bool Success or not.</returns>  
        /// </summary>
        bool TryDeletePokemon(string name);
        /// <summary>Only for internal class use.
        /// <param name="name">The name of the Pokemon to delete.</param>
        /// </summary>
        bool DeletePokemon(string name);
    }
}
