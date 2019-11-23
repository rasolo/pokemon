using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Core.Entities
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }
        public int Index { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Types { get; set; }
        public ICollection<Evolution> Evolutions { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}