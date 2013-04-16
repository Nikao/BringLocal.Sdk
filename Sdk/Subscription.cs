using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace BringLocal.Sdk
{
    public class Subscription : ApiResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("originalMessage")]
        public string OriginalMessage { get; set; }
        [JsonProperty("doubleOptIn")]
        public bool DoubleOptIn { get; set; }

        public Subscription()
        {
            
        }
        public Subscription(IRestResponse response)
        {
            StatusCode = response.StatusCode;

            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP: nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }
    }
}