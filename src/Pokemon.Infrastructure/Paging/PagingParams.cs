using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Pokemon.Infrastructure.Paging
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Direction { get; set; }
        public string Sort { get; set; }
    }

    public class PagingHeader
    {
        public PagingHeader(
            int totalItems, int pageNumber, int pageSize, int totalPages)
        {
            this.TotalItems = totalItems;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public string ToJson() => JsonConvert.SerializeObject(this,
            new JsonSerializerSettings
            {
                ContractResolver = new
                    CamelCasePropertyNamesContractResolver()
            });

    }
}
