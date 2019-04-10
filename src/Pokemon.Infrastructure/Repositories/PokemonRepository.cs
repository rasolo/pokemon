using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Core.Paging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Infrastructure.Data;
using Pokemon.Core.Extensions;
using System.Linq;

namespace Pokemon.Api.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext _dbContext;
        public PokemonRepository(PokemonContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPokemon(Core.Entities.Pokemon pokemon)
        {
            _dbContext.Set<Core.Entities.Pokemon>().Add(pokemon);
            _dbContext.SaveChanges();
        }

        public Core.Entities.Pokemon GetByName(string name)
        {
            var pokemon = _dbContext.Pokemon.Include(x => x.Moves).Include(x => x.Evolutions).SingleOrDefault(x => x.Name == name.FirstLetterToUpper());

            return pokemon;
        }

        public PagedList<Api.Core.Entities.Pokemon> GetPokemons(PagingParams pagingParams)
        {
            var query = _dbContext.Pokemon.AsQueryable().Include(x => x.Moves).Include(x => x.Evolutions);
            var pagedList = new PagedList<Core.Entities.Pokemon>(
                query, pagingParams.PageNumber, pagingParams.PageSize);


            return pagedList;
        }
    }
}
