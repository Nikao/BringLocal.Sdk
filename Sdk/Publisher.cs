using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Publisher : ApiResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("acceptedCreditCards")]
        public List<AcceptedCreditCard> AcceptedCreditCards { get; set; }

        public Publisher()
        {
            AcceptedCreditCards = new List<AcceptedCreditCard>();
        }

        public Publisher(IRestResponse response) : this()
        {
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<Publisher> Fetch(Guid id)
        {
            var request = ClientHelper.Request("publishers/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());

            var tcs = new TaskCompletionSource<Publisher>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new Publisher(response));
                }
                else
                {
                    tcs.SetException(response.ErrorException);
                }
            });
            return tcs.Task;
        }
    }
}