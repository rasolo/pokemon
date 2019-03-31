using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Pokemon.Core.Entities
{
   public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }
        public int Index { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public List<string> Types { get; set; }
        /// <summary> <see cref="MetaData"/> for database persistence. </summary>
        [Obsolete("Only for Persistence by EntityFramework")]
        public string MetaDataJsonForDb
        {
            get
            {
                return Types == null || !Types.Any()
                           ? null
                           : JsonConvert.SerializeObject(Types);
            }

            set
            {
                if(Types == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                    Types.Clear();
                else
                    Types = JsonConvert.DeserializeObject<List<string>>(value);
            }
        }
        public virtual ICollection<Evolution> Evolutions { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }

    public class Evolution
    {
        [Key]
        public int Id { get; set; }
        public int Pokemon { get; set; }
        public string Event { get; set; }
    }

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
