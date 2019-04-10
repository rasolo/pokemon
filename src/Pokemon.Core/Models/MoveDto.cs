namespace Pokemon.Api.Core.Models

{
    public class MoveDto
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
}
