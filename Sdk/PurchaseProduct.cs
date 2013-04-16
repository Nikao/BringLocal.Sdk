using Newtonsoft.Json;
using System;

namespace BringLocal.Sdk
{
    public class PurchaseProduct
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}