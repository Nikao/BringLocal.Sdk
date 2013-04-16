using Newtonsoft.Json;

namespace BringLocal.Sdk
{
    public class OfferImages
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
    }
}
