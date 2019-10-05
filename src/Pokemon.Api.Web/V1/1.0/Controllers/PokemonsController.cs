using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Core.Exceptions;
using Pokemon.Api.Core.Extensions;
using Pokemon.Api.Core.Paging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Core.Services;
using Pokemon.Api.Web.Filters;
using Pokemon.Api.Web.V1._1._0.Models;

namespace Pokemon.Api.Web.V1._1._0.Controllers
{
    //[ApiVersion(ApiVersion)]
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

        [HttpPost("{add}", Name = nameof(AddPokemon))]
        [ValidatePokemonDtoModel]
        public IActionResult AddPokemon([FromBody] PokemonForCreationDto pokemonForCreateDto)
        {
            var pokemon = _mapper.Map<Core.Entities.Pokemon>(pokemonForCreateDto);
            _pokemonRepository.AddPokemon(pokemon);

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);
            var genericApiResponse = new GenericApiResponse<Core.Entities.Pokemon> { Data = pokemon };

            return Created($"{ControllerRoute}/{nameof(GetPokemon)}", genericApiResponse);
        }

        [HttpGet("{name}", Name = nameof(GetPokemon))]
        public GenericApiResponse<PokemonDto> GetPokemon(string name)
        {
            Core.Entities.Pokemon pokemon = _pokemonRepository.GetByName(name);
            var pokemonDto = new PokemonDto();
            this._mapper.Map(pokemon, pokemonDto);
            var genericApiResponse = new GenericApiResponse<PokemonDto> { Data = pokemonDto };

            if (pokemon == null)
            {
                genericApiResponse.Success = false;
                genericApiResponse.ErrorMessage = ApiErrors.NotFound.GetDescription();
                throw new ApiException(ApiErrors.NotFound);
            }


            return genericApiResponse;
        }

        [HttpDelete("{name}", Name = nameof(DeletePokemon))]
        public IActionResult DeletePokemon(string name)
        {
            var genericApiResponse = new GenericApiResponse<string>();
            bool success = _pokemonRepository.TryDeletePokemon(name);
            genericApiResponse.Success = success;

            if (!success)
            {
                genericApiResponse.ErrorMessage = ApiErrors.DeletionFailure.GetDescription();
                throw new ApiException(ApiErrors.DeletionFailure);
            }

            genericApiResponse.Data = $"{name} deleted.";

            return Ok(genericApiResponse);
        }

        [HttpGet("list", Name = nameof(GetPokemons))]
        public GenericApiResponse<ObjectDto> GetPokemons(PagingParams pagingParams)
        {
            var genericApiResponse = new GenericApiResponse<ObjectDto>();

            if (pagingParams == null)
            {
                genericApiResponse.Success = false;
                genericApiResponse.ErrorMessage = ApiErrors.BadRequest.GetDescription();
                throw new ApiException(ApiErrors.BadRequest);
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
                outputModel.Pokemons = orderedPokemons.Select(x => this._mapper.Map(x, new PokemonDto())).AsQueryable().OrderBy(query);
            }
            else
            {
                outputModel.Pokemons = orderedPokemons.Select(x => this._mapper.Map(x, new PokemonDto())).AsQueryable().OrderBy(x => x.name);
            }

            genericApiResponse.Data = outputModel;

            return genericApiResponse;
        }
    }
}
