using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class CreditCard
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }

        internal CreditCard(dynamic item)
        {
            Id = new Guid(item.id);
            Description = item.description;
        }
    }
}