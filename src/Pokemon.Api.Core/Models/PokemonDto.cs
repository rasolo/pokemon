using System.Collections.Generic;

namespace Pokemon.Api.Core.Models
{
    public class PokemonDto
    {
        public int index { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public List<string> types { get; set; }
        public List<EvolutionDto> evolutions { get; set; }
        public List<MoveDto> moves { get; set; }

        public static PokemonDto FromPokemon(Entities.Pokemon pokemon)
        {
            var pokemonDto =
            new PokemonDto()
            {
                index = pokemon.Index,
                name = pokemon.Name,
                image_url = pokemon.ImageUrl,
                types = pokemon.Types,
                evolutions =
                new List<EvolutionDto>(),
                moves = new List<MoveDto>()
            };
            var evolutionEnumerator = pokemon.Evolutions.GetEnumerator();
            while (evolutionEnumerator.MoveNext())
            {
                pokemonDto.evolutions.Add(
                    new EvolutionDto
                    {
                        Event = evolutionEnumerator.Current.Event,
                        Pokemon = evolutionEnumerator.Current.Pokemon
                    }
                    );
            };

            var movesEnumerator = pokemon.Moves.GetEnumerator();
            while (movesEnumerator.MoveNext())
            {
                pokemonDto.moves.Add(
                    new MoveDto
                    {
                        accuracy = movesEnumerator.Current.Accuracy,
                        attack = movesEnumerator.Current.Attack,
                        category = movesEnumerator.Current.Category,
                        description = movesEnumerator.Current.Description,
                        effect_percent = movesEnumerator.Current.EffectPercent,
                        level = movesEnumerator.Current.Level,
                        name = movesEnumerator.Current.Name,
                        pp = movesEnumerator.Current.PP,
                        type = movesEnumerator.Current.Type
                    }
                );
            };

            return pokemonDto;
        }
        public static Entities.Pokemon ToPokemon(PokemonDto pokemonDto)
        {
            var pokemon = new Entities.Pokemon
            {
                Name = pokemonDto.name,
                Index = pokemonDto.index,
                ImageUrl = pokemonDto.image_url,
                Types = pokemonDto.types,
                Evolutions = new List<Entities.Evolution>(),
                Moves = new List<Entities.Move>()
            };

            pokemonDto.evolutions.ForEach(x =>
            {
                pokemon.Evolutions.Add
                (
                     new Entities.Evolution()
                     {
                         Event = x.Event,
                         Pokemon = x.Pokemon
                     }
                 );
            }
            );

            pokemonDto.moves.ForEach(x =>
            {
                pokemon.Moves.Add
                (
                    new Entities.Move()
                    {
                        Accuracy = x.accuracy,
                        Attack = x.attack,
                        Category = x.category,
                        Description = x.description,
                        EffectPercent = x.effect_percent,
                        Level = x.level,
                        Name = x.name,
                        PP = x.pp,
                        Type = x.type,
                    }
                );
            });

            return pokemon;
        }

    }
}
