using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pokemon.Infrastructure.Paging;
using Pokemon.Infrastructure.Repositories;
using System.Linq;
using System.Collections.Generic;
using Pokemon.Api.Models;

namespace Pokemon.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pokemon")]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet("{name}", Name = "GetPokemon")]
        public IActionResult GetPokemon(string name)
        {
            Core.Entities.Pokemon pokemon = _pokemonRepository.GetByName(name);

            if (pokemon == null)
            {
                return NotFound();
            }

            var pokemonDto = _mapper.Map(pokemon, new Api.Models.PokemonDto());

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
            IEnumerable<Pokemon.Core.Entities.Pokemon> orderedSites = pokemonEntities.List.OrderBy(s => s.Name).ToList();
            Response.Headers.Add("X-Pagination", pokemonEntities.GetHeader().ToJson());
            var outputModel = new ObjectDto
            {
                Paging = pokemonEntities.GetHeader(),
                Pokemons =  pokemonEntities.List.Select(_mapper.Map<Models.PokemonDto>).AsQueryable()
            };


            return Ok(outputModel);
        }
    }
}
