using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pokemon.Core.Paging;
using System.Linq;
using System.Collections.Generic;
using Pokemon.Api.Models;
using Pokemon.Core.Services;
using System.Linq.Dynamic.Core;
using Pokemon.Core.Models;
using Pokemon.Core.Repositories;
using Pokemon.Api.Filters;

namespace Pokemon.Api.Controllers
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
            var pokemon = _mapper.Map<Pokemon.Core.Entities.Pokemon>(pokemonForCreateDto);
            _pokemonRepository.AddPokemon(pokemon);

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);

            return CreatedAtAction(nameof(GetPokemon), new { name = pokemon.Name }, pokemon);
        }

        [HttpGet("{name}", Name = "GetPokemon")]
        public IActionResult GetPokemon(string name)
        {
            Core.Entities.Pokemon pokemon = _pokemonRepository.GetByName(name);

            if (pokemon == null)
            {
                return NotFound();
            }

            var pokemonDto = _mapper.Map(pokemon, new PokemonDto());

            return Ok(pokemonDto);
        }

        [HttpGet("list", Name = "GetPokemons")]
        public IActionResult GetPokemons(PagingParams pagingParams)
        {
            if (pagingParams == null)
            {
                return BadRequest();
            }


            var pokemonEntities =  _pokemonRepository.GetPokemons(pagingParams);
            IEnumerable<Pokemon.Core.Entities.Pokemon> orderedPokemons = pokemonEntities.List.OrderBy(s => s.Name).ToList();
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


            return Ok(outputModel);
        }

    
    }
}
