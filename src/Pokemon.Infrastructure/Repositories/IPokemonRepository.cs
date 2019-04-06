
using Pokemon.Infrastructure.Data;
using Pokemon.Infrastructure.Paging;


namespace Pokemon.Infrastructure.Repositories
{
    
  public interface IPokemonRepository
    {
        Core.Entities.Pokemon GetByName(string name);
        PagedList<Core.Entities.Pokemon> GetPokemons(PagingParams pagingParams);
    }
}
