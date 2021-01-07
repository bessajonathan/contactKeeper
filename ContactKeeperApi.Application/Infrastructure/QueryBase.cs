using Newtonsoft.Json;
using NSwag.Annotations;

namespace ContactKeeperApi.Application.Infrastructure
{
    public class QueryBase
    {
        [JsonIgnore]
        [OpenApiIgnore]
        public int UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;

        private string _orderBy;
        public string OrderBy
        {
            get
            {
                if (_orderBy == null || _orderBy == string.Empty)
                    return "createdAt";

                return _orderBy;
            }
            set => _orderBy = value;
        }
    }
}
