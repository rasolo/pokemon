using System.Linq;
using Pokemon.Core.Extensions;
namespace Pokemon.Infrastructure.Data
{
   public class PokemonRepository : Core.Contracts.IPokemonRepository
    {
        private readonly AppDbContext _dbContext;
        public PokemonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Core.Entities.Pokemon GetByName(string name)
        {
            var pokemon = _dbContext.Pokemon.SingleOrDefault(x => x.Name == name.FirstLetterToUpper());
            return pokemon;
        }
    }
}
