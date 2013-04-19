using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class OffersCollection : ApiResponse
    {
        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }
        public OffersCollection()
        {
            Offers = new List<Offer>();

        }
        public OffersCollection(IRestResponse response) : this()
        {
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, Offers);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP - nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<OffersCollection> Fetch(Guid publisherId, Guid siteId)
        {
            var request = ClientHelper.Request("offers", Method.GET);
            request.AddParameter("PublisherId", publisherId);
            request.AddParameter("SiteId", siteId);

            var tcs = new TaskCompletionSource<OffersCollection>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new OffersCollection(response));
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