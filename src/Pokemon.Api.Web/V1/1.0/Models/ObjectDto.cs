using Pokemon.Api.Core.Paging;
using System.Collections.Generic;

namespace Pokemon.Api.Web.V1.Models
{
    public class ObjectDto
    {
        public PagingHeader Paging { get; set; }
        public IEnumerable<PokemonDto> Pokemons { get; set; }
    }
}
