using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Core.Extensions;
using Pokemon.Api.Core.Paging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Infrastructure.Data;

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

        public bool TryDeletePokemon(string name)
        {
            try
            {
                return DeletePokemon(name);
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePokemon(string name)
        {
            var pokemon = GetByName(name);

            if (pokemon == null)
            {
                return false;
            }

            _dbContext.Remove(pokemon);
            _dbContext.SaveChanges();
            return true;
        }

        public Core.Entities.Pokemon GetByName(string name)
        {
            var pokemon = _dbContext.Pokemon.Include(x => x.Moves).Include(x => x.Evolutions)
                .SingleOrDefault(x => x.Name == name.FirstLetterToUpper());

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