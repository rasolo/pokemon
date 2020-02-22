using System.Collections.Generic;
using Pokemon.Api.Core.Paging;

namespace Pokemon.Api.Web.Models
{
    public class ObjectDto
    {
        public PagingHeader Paging { get; set; }
        public IEnumerable<PokemonDto> Pokemons { get; set; }
    }
}