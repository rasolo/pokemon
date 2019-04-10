using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Core.Models;
using Pokemon.Api.Core.Paging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Core.Services;
using Pokemon.Api.Web.Filters;
using Pokemon.Api.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Pokemon.Api.Web.Controllers
{
    [ApiVersion(ApiVersion)]
    [Route(ControllerRoute)]
    public class PokemonsController : Controller
    {
        private const string ApiVersion = "1.0";
        private const string ControllerRoute = "api/v" + ApiVersion + "/pokemon"; //api/v1.0/pokemon
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonService _pokemonService;

        public PokemonsController(IPokemonRepository pokemonRepository, IMapper mapper, IPokemonService pokemonService)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _pokemonService = pokemonService;
        }

        [HttpPost("{add}", Name = "AddPokemon")]
        [ValidatePokemonDtoModel]
        public IActionResult AddPokemon([FromBody] PokemonForCreationDto pokemonForCreateDto)
        {
            var pokemon = _mapper.Map<Core.Entities.Pokemon>(pokemonForCreateDto);
            _pokemonRepository.AddPokemon(pokemon);

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);

            return CreatedAtAction(nameof(GetPokemon), new { name = pokemon.Name }, pokemon);
        }

        [HttpGet("{name}", Name = "GetPokemon")]
        public GenericApiResponse<PokemonDto> GetPokemon(string name)
        {
            Core.Entities.Pokemon pokemon = _pokemonRepository.GetByName(name);
            var pokemonDto = _mapper.Map(pokemon, new PokemonDto());
            var genericApiResponse = new GenericApiResponse<PokemonDto> { Data = pokemonDto };

            if (pokemon == null)
            {
                genericApiResponse.Success = false;
                genericApiResponse.ErrorMessage = "404 Not Found";
            }


            return genericApiResponse;
        }

        [HttpDelete("{name}", Name = "DeletePokemon")]
        public IActionResult DeletePokemon(string name)
        {
            List<string> s = new List<string>();
            var genericApiResponse = new GenericApiResponse<string>();
            bool success = _pokemonRepository.TryDeletePokemon(name);
            genericApiResponse.Success = success;

            if (!success)
            {
                genericApiResponse.ErrorMessage = $"Unable to delete {name}";
                return StatusCode(500, genericApiResponse);
            }

            genericApiResponse.Data = $"{name} deleted.";

            return Ok(genericApiResponse);
        }

        [HttpGet("list", Name = "GetPokemons")]
        public GenericApiResponse<ObjectDto> GetPokemons(PagingParams pagingParams)
        {
            var genericApiResponse = new GenericApiResponse<ObjectDto>();

            if (pagingParams == null)
            {
                genericApiResponse.Success = false;
                genericApiResponse.ErrorMessage = "400 Bad Request";
                return genericApiResponse;
            }


            var pokemonEntities = _pokemonRepository.GetPokemons(pagingParams);
            IEnumerable<Core.Entities.Pokemon> orderedPokemons = pokemonEntities.List.OrderBy(s => s.Name).ToList();
            Response?.Headers.Add("X-Pagination", pokemonEntities.GetHeader().ToJson());
            var outputModel = new ObjectDto
            {
                Paging = pokemonEntities.GetHeader(),
            };

            var query = _pokemonService.GetFilteredSortQuery(pagingParams.Sort);


            if (query != null)
            {
                outputModel.Pokemons = orderedPokemons.Select(x => PokemonDto.FromPokemon(x)).AsQueryable().OrderBy(query);
            }
            else
            {
                outputModel.Pokemons = orderedPokemons.Select(x => PokemonDto.FromPokemon(x)).AsQueryable().OrderBy(x => x.name);
            }

            genericApiResponse.Data = outputModel;

            return genericApiResponse;
        }
    }
}
