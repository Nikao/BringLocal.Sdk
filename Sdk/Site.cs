using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Site : ApiResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("acceptedCreditCards")]
        public List<AcceptedCreditCard> AcceptedCreditCards { get; set; }

        public Site()
        {
            AcceptedCreditCards = new List<AcceptedCreditCard>();
        }

        public Site(IRestResponse response) : this()
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

        public static Task<Site> Fetch(Guid id)
        {
            var request = ClientHelper.Request("sites/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());

            var tcs = new TaskCompletionSource<Site>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new Site(response));
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