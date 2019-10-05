using System;
using System.Collections.Generic;
using System.Text.Json;
using Pokemon.Api.Web.V1._1._0.Models;

namespace Pokemon.Api.Web.Services
{
    public static class JsonDocumentService
    {
        private static readonly Dictionary<string, string> PokemonDtoMagicStringProperties = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> EvolutionDtoMagicStringProperties = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> MoveDtoMagicStringProperties = new Dictionary<string, string>();
        static JsonDocumentService()
        {
            PokemonDtoMagicStringProperties.Add("index", "index");
            PokemonDtoMagicStringProperties.Add("name", "name");
            PokemonDtoMagicStringProperties.Add("image_url", "image_url");
            PokemonDtoMagicStringProperties.Add("types", "types");
            PokemonDtoMagicStringProperties.Add("evolutions", "evolutions");
            PokemonDtoMagicStringProperties.Add("moves", "moves");

            EvolutionDtoMagicStringProperties.Add("pokemon", "pokemon");
            EvolutionDtoMagicStringProperties.Add("event", "event");

            MoveDtoMagicStringProperties.Add("level", "level");
            MoveDtoMagicStringProperties.Add("name", "name");
            MoveDtoMagicStringProperties.Add("type", "type");
            MoveDtoMagicStringProperties.Add("category", "category");
            MoveDtoMagicStringProperties.Add("attack", "attack");
            MoveDtoMagicStringProperties.Add("accuracy", "accuracy");
            MoveDtoMagicStringProperties.Add("pp", "pp");
            MoveDtoMagicStringProperties.Add("effect_percent", "effect_percent");
            MoveDtoMagicStringProperties.Add("description", "description");

            CheckPropertiesHasChanged();
        }
        private static void CheckPropertiesHasChanged()
        {
            var pokemonDto = new PokemonDto();
            var pokemonDtoProperties = pokemonDto.GetType().GetProperties();

            foreach (var prop in pokemonDtoProperties)
            {
                if (!PokemonDtoMagicStringProperties.ContainsValue(prop.Name))
                {
                    throw new Exception("PokemonDto's properties has changed");
                }
            }

            var evolutionDto = new EvolutionDto();
            var evolutionDtoProperties = evolutionDto.GetType().GetProperties();

            foreach (var prop in evolutionDtoProperties)
            {
                if (!EvolutionDtoMagicStringProperties.ContainsValue(prop.Name?.ToLower()))
                {
                    throw new Exception("EvolutionDto's properties has changed");
                }
            }

            var moveDto = new MoveDto();
            var moveDtoProperties = moveDto.GetType().GetProperties();

            foreach (var prop in moveDtoProperties)
            {
                if (!MoveDtoMagicStringProperties.ContainsValue(prop.Name))
                {
                    throw new Exception("MoveDto's properties has changed");
                }
            }


        }
        public static PokemonDto ConvertToPokemonDto(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;
            int index = root.GetProperty(PokemonDtoMagicStringProperties["index"]).GetInt32();
            string name = root.GetProperty(PokemonDtoMagicStringProperties["name"]).GetString();
            string imageUrl = root.GetProperty(PokemonDtoMagicStringProperties["image_url"]).GetString();
            JsonElement types = root.GetProperty(PokemonDtoMagicStringProperties["types"]);
            JsonElement evolutions = root.GetProperty(PokemonDtoMagicStringProperties["evolutions"]);
            JsonElement moves = root.GetProperty(PokemonDtoMagicStringProperties["moves"]);


            var pokemonDto = new PokemonDto
            {
                index = index,
                name = name,
                image_url = imageUrl,
                types = new List<string>(),
                evolutions = new List<EvolutionDto>(),
                moves = new List<MoveDto>()
            };

            var typesEnumerator = types.EnumerateArray().GetEnumerator();
            while (typesEnumerator.MoveNext())
            {
                pokemonDto.types.Add(typesEnumerator.Current.ToString());
            }
            var evolutionsEnumerator = evolutions.EnumerateArray().GetEnumerator();
            while (evolutionsEnumerator.MoveNext())
            {
                var evolutionDto = new EvolutionDto();
                evolutionDto.Pokemon = evolutionsEnumerator.Current.GetProperty(EvolutionDtoMagicStringProperties["pokemon"]).GetInt32();
                evolutionDto.Event = evolutionsEnumerator.Current.GetProperty(EvolutionDtoMagicStringProperties["event"]).GetString();
                pokemonDto.evolutions.Add(evolutionDto);

            }
            var movesEnumerator = moves.EnumerateArray().GetEnumerator();
            while (movesEnumerator.MoveNext())
            {
                MoveDto moveDto = new MoveDto();
                moveDto.level = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["level"]).GetString();
                moveDto.name = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["name"]).GetString();
                moveDto.type = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["type"]).GetString();
                moveDto.category = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["category"]).GetString();
                moveDto.attack = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["attack"]).GetString();
                moveDto.accuracy = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["accuracy"]).GetString();
                moveDto.pp = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["pp"]).GetString();
                moveDto.effect_percent = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["effect_percent"]).GetString();
                moveDto.description = movesEnumerator.Current.GetProperty(MoveDtoMagicStringProperties["description"]).GetString();
                pokemonDto.moves.Add(moveDto);
            }

            return pokemonDto;
        }
    }
}
