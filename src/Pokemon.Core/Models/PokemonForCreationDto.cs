using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Core.Models
{
    public class PokemonForCreationDto
    {
        public int PokemonId { get; set; }
        public int Index { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public List<string> Types { get; set; }
        public ICollection<EvolutionDto> Evolutions { get; set; }
        public ICollection<MoveDto> Moves { get; set; }
    }
}
