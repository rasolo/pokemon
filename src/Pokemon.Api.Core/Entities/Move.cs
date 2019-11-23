using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Core.Entities
{
    public class Move
    {
        [Key]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Attack { get; set; }
        public string Accuracy { get; set; }
        public string PP { get; set; }
        public string EffectPercent { get; set; }
        public string Description { get; set; }
    }
}