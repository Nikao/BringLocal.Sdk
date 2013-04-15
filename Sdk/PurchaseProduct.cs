using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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