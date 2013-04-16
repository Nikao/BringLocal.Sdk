using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace BringLocal.Sdk
{
    public class UserToken : ApiResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expires")]
        public DateTime Expires { get; set; }

        internal UserToken(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                default:
                    this.DeserializeErrors(response.Content);
                    break;
            }
        }
    }
}
