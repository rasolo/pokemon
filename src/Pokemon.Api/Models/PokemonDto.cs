using System.Collections.Generic;

namespace Pokemon.Api.Models
{
    public class Evolution
    {
        public int pokemon { get; set; }
        public string @event { get; set; }
    }

    public class Move
    {
        public string level { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string attack { get; set; }
        public string accuracy { get; set; }
        public string pp { get; set; }
        public string effect_percent { get; set; }
        public string description { get; set; }
    }

    public class PokemonDto
    {
        public int index { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public List<string> types { get; set; }
        public List<Evolution> evolutions { get; set; }
        public List<Move> moves { get; set; }
    }
}
