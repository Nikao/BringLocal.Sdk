using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class UserAccountBalance
    {
        [JsonProperty("publisherId")]
        public Guid PublisherId { get; set; }
        [JsonProperty("accountBalance")]
        public Decimal AccountBalance { get; set; }
    }
}
