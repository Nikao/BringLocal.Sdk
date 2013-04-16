using Newtonsoft.Json;
using System;

namespace BringLocal.Sdk
{
    public class CreditCard
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }
    }
}