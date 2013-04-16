using Newtonsoft.Json;
using System;

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
