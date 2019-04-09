using Pokemon.Core.Models;
using Pokemon.Core.Paging;
using System.Collections.Generic;

namespace Pokemon.Api.Models
{
    public class ObjectDto
    {
        public PagingHeader Paging { get; set; }
        public IEnumerable<PokemonDto> Pokemons { get; set; }
    }
}
