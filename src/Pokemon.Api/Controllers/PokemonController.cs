using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Pokemon.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pokemon")]
    public class PokemonController : Controller
    {
        private readonly Core.Contracts.IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(Core.Contracts.IPokemonRepository pokemonRepository, IMapper mapper)
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
    }
}
