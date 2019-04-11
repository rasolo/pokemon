using Pokemon.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pokemon.Api.Core.Services
{
    public static class JsonDocumentService
    {
        private static readonly Dictionary<string, string> _pokemonDtoMagicStringProperties = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _evolutionDtoMagicStringProperties = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _moveDtoMagicStringProperties = new Dictionary<string, string>();
        static JsonDocumentService()
        {
            _pokemonDtoMagicStringProperties.Add("index", "index");
            _pokemonDtoMagicStringProperties.Add("name", "name");
            _pokemonDtoMagicStringProperties.Add("image_url", "image_url");
            _pokemonDtoMagicStringProperties.Add("types", "types");
            _pokemonDtoMagicStringProperties.Add("evolutions", "evolutions");
            _pokemonDtoMagicStringProperties.Add("moves", "moves");

            _evolutionDtoMagicStringProperties.Add("pokemon", "pokemon");
            _evolutionDtoMagicStringProperties.Add("event", "event");

            _moveDtoMagicStringProperties.Add("level", "level");
            _moveDtoMagicStringProperties.Add("name", "name");
            _moveDtoMagicStringProperties.Add("type", "type");
            _moveDtoMagicStringProperties.Add("category", "category");
            _moveDtoMagicStringProperties.Add("attack", "attack");
            _moveDtoMagicStringProperties.Add("accuracy", "accuracy");
            _moveDtoMagicStringProperties.Add("pp", "pp");
            _moveDtoMagicStringProperties.Add("effect_percent", "effect_percent");
            _moveDtoMagicStringProperties.Add("description", "description");

            CheckPropertiesHasChanged();
        }
        private static void CheckPropertiesHasChanged()
        {
            var pokemonDto = new PokemonDto();
            var pokemonDtoProperties = pokemonDto.GetType().GetProperties();

            foreach (var prop in pokemonDtoProperties)
            {
                if (!_pokemonDtoMagicStringProperties.ContainsValue(prop.Name))
                {
                    throw new Exception("PokemonDto's properties has changed");
                }
            }

            var evolutionDto = new EvolutionDto();
            var evolutionDtoProperties = evolutionDto.GetType().GetProperties();

            foreach (var prop in evolutionDtoProperties)
            {
                if (!_evolutionDtoMagicStringProperties.ContainsValue(prop.Name?.ToLower()))
                {
                    throw new Exception("EvolutionDto's properties has changed");
                }
            }

            var moveDto = new MoveDto();
            var moveDtoProperties = moveDto.GetType().GetProperties();

            foreach (var prop in moveDtoProperties)
            {
                if (!_moveDtoMagicStringProperties.ContainsValue(prop.Name))
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
            int index = root.GetProperty(_pokemonDtoMagicStringProperties["index"]).GetInt32();
            string name = root.GetProperty(_pokemonDtoMagicStringProperties["name"]).GetString();
            string imageUrl = root.GetProperty(_pokemonDtoMagicStringProperties["image_url"]).GetString();
            JsonElement types = root.GetProperty(_pokemonDtoMagicStringProperties["types"]);
            JsonElement evolutions = root.GetProperty(_pokemonDtoMagicStringProperties["evolutions"]);
            JsonElement moves = root.GetProperty(_pokemonDtoMagicStringProperties["moves"]);


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
                evolutionDto.Pokemon =  evolutionsEnumerator.Current.GetProperty(_evolutionDtoMagicStringProperties["pokemon"]).GetInt32();
                evolutionDto.Event = evolutionsEnumerator.Current.GetProperty(_evolutionDtoMagicStringProperties["event"]).GetString();
                pokemonDto.evolutions.Add(evolutionDto);

            }
            var movesEnumerator = moves.EnumerateArray().GetEnumerator();
            while (movesEnumerator.MoveNext())
            {
                MoveDto moveDto = new MoveDto();
                moveDto.level = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["level"]).GetString();
                moveDto.name = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["name"]).GetString();
                moveDto.type = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["type"]).GetString();
                moveDto.category = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["category"]).GetString();
                moveDto.attack = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["attack"]).GetString();
                moveDto.accuracy = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["accuracy"]).GetString();
                moveDto.pp = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["pp"]).GetString();
                moveDto.effect_percent = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["effect_percent"]).GetString();
                moveDto.description = movesEnumerator.Current.GetProperty(_moveDtoMagicStringProperties["description"]).GetString();
                pokemonDto.moves.Add(moveDto);
            }

            return pokemonDto;
        }
    }
}
