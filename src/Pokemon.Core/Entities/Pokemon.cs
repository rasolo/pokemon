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
        public ICollection<Evolution> Evolutions { get; set; }
        public ICollection<Move> Moves { get; set; }
    }
}
