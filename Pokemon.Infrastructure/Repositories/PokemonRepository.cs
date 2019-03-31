using System.Linq;
using Pokemon.Core.Entities;
using Pokemon.Core.Extensions;
using Pokemon.Infrastructure.Paging;
using Pokemon.Infrastructure.Repositories;

namespace Pokemon.Infrastructure.Data
{
   public class PokemonRepository : IPokemonRepository
    {
        private readonly AppDbContext _dbContext;
        public PokemonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Core.Entities.Pokemon GetByName(string name)
        {
            var pokemon = _dbContext.Pokemon.SingleOrDefault(x => x.Name == name.FirstLetterToUpper());
            if(pokemon != null)
            {
              pokemon.Evolutions = _dbContext.Evolution.Where(e => e.PokemonId == pokemon.PokemonId).ToList();
              pokemon.Moves = _dbContext.Move.Where(m => m.PokemonId == pokemon.PokemonId).ToList();
            }
            return pokemon;
        }

        public PagedList<Core.Entities.Pokemon> GetPokemons(PagingParams pagingParams)
        {
            var query = _dbContext.Pokemon.AsQueryable();
            return new PagedList<Core.Entities.Pokemon>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
