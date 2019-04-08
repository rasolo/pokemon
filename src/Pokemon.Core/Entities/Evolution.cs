using System.ComponentModel.DataAnnotations;

namespace Pokemon.Core.Entities
{
    public class Evolution
    {
        [Key]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int Pokemon { get; set; }
        public string Event { get; set; }
    }
}
