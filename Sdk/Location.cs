using Newtonsoft.Json;
using System;

namespace BringLocal.Sdk
{
    public class Location
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
}
