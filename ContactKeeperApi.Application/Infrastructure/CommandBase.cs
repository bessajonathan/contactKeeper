using Newtonsoft.Json;
using NSwag.Annotations;

namespace ContactKeeperApi.Application.Infrastructure
{
    public class CommandBase
    {
        [JsonIgnore]
        [OpenApiIgnore]
        public int UserId { get; set; }
    }
}
