using System.Collections.Generic;
using Pokemon.Api.Core.Paging;

namespace Pokemon.Api.Web.V1._1._0.Models
{
    public class ObjectDto
    {
        public PagingHeader Paging { get; set; }
        public IEnumerable<PokemonDto> Pokemons { get; set; }
    }
}