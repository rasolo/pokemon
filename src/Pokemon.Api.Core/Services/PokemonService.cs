using System.Collections.Generic;
using System.Linq;
using Pokemon.Api.Core.Extensions;
using Pokemon.Api.Core.Repositories;

namespace Pokemon.Api.Core.Services
{
    public class PokemonService : IPokemonService
    {
        public PokemonService(IPokemonRepository pokemonRepository)
        {
            PokemonRepository = pokemonRepository;
        }

        public IPokemonRepository PokemonRepository { get; }

        public string GetFilteredSortQuery(string sortQuery)
        {
            if (string.IsNullOrEmpty(sortQuery))
            {
                return null;
            }

            var validDirections = new[] {"asc", "ascending", "desc", "descending"};
            var pokemonPropertyNames = GetPokemonPropertyNames(new Entities.Pokemon());
            var splitSortQuery = sortQuery.Split(' ');

            var sortProperty =
                pokemonPropertyNames.SingleOrDefault(x => x.FirstLetterToLower().Equals(splitSortQuery[0]));

            if (sortProperty == null)
            {
                return null;
            }

            sortProperty = sortProperty.FirstLetterToLower();

            //only one param
            if (splitSortQuery.Length <= 1)
            {
                return sortProperty;
            }

            var direction = validDirections.SingleOrDefault(x => x.FirstLetterToLower().Equals(splitSortQuery[1]));

            return $"{sortProperty} {direction}";
        }

        public bool NameIsUnique(string name)
        {
            return PokemonRepository.GetByName(name)?.Name != name;
        }

        public IEnumerable<string> GetPokemonPropertyNames(object pokemon)
        {
            foreach (var prop in pokemon.GetType().GetProperties()) yield return prop.Name;
        }
    }
}