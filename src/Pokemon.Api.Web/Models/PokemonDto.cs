﻿using System.Collections.Generic;

namespace Pokemon.Api.Web.Models
{
    public class PokemonDto
    {
        public int index { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public List<string> types { get; set; }
        public List<EvolutionDto> evolutions { get; set; }
        public List<MoveDto> moves { get; set; }
    }
}