using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Pokemon.Core.Paging
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Direction { get; set; }
        public string Sort { get; set; }
    }

   
}
