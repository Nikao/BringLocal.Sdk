using Newtonsoft.Json;

namespace BringLocal.Sdk
{
    public class AcceptedCreditCard
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
