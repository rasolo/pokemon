using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var pokemon = _dbContext.Pokemon.Include(x => x.Moves).Include(x => x.Evolutions).SingleOrDefault(x => x.Name == name.FirstLetterToUpper());

            return pokemon;
        }

        public PagedList<Core.Entities.Pokemon> GetPokemons(PagingParams pagingParams)
        {
            var query = _dbContext.Pokemon.AsQueryable().Include(x => x.Moves).Include(x => x.Evolutions);
            var pagedList = new PagedList<Core.Entities.Pokemon>(
                query, pagingParams.PageNumber, pagingParams.PageSize);


            return pagedList;
        }
    }
}
