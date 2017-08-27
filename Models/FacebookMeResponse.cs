using Newtonsoft.Json;

namespace DinnerTime.Api.Models
{
    public class FacebookMeResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("error")]
        public dynamic Error { get; set; }
    }
}
