using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pokemon.Infrastructure.Paging;
using Pokemon.Infrastructure.Repositories;
using System.Linq;
using System.Collections.Generic;
using Pokemon.Api.Models;
using Pokemon.Core.Services;
using System.Linq.Dynamic.Core;
using Pokemon.Core.Models;

namespace Pokemon.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pokemon")]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper, IPokemonService pokemonService)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _pokemonService = pokemonService;
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
            Response.Headers.Add("X-Pagination", pokemonEntities.GetHeader().ToJson());
            var outputModel = new ObjectDto
            {
                Paging = pokemonEntities.GetHeader(),
            };

            var query = _pokemonService.GetFilteredSortQuery(pagingParams.Sort);


            if (query != null)
            {
                outputModel.Pokemons = orderedPokemons.Select(_mapper.Map<PokemonDto>).AsQueryable().OrderBy(query);
            }
            else
            {
                outputModel.Pokemons = orderedPokemons.Select(_mapper.Map<PokemonDto>).AsQueryable().OrderBy(x => x.name);
            }


            return Ok(outputModel);
        }
    }
}
