using Pokemon.Core.Extensions;
using Pokemon.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Core.Services
{
    public class PokemonService : IPokemonService
    {
        public IPokemonRepository _pokemonRepository { get; }

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public IEnumerable<string> GetPokemonPropertyNames(object pokemon)
        {
            foreach (var prop in pokemon.GetType().GetProperties())
            {
                yield return prop.Name;
            }
        }

        public string GetFilteredSortQuery(string sortQuery)
        {
            if (string.IsNullOrEmpty(sortQuery))
            {
                return null;
            }

            var validDirections = new[] { "asc", "ascending", "desc", "descending" }; //valid directions for dynamic linq
            var pokemonPropertyNames = GetPokemonPropertyNames(new Pokemon.Core.Entities.Pokemon());
            var splitSortQuery = sortQuery.Split(' ');

            var sortProperty = pokemonPropertyNames.SingleOrDefault(x => x.FirstLetterToLower().Equals(splitSortQuery[0]));

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
            return _pokemonRepository.GetByName(name)?.Name != name;
        }
    }
}
