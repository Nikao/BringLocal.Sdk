using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class OffersCollection : ApiResponse
    {
        public List<Offer> Offers { get; set; }
        private OffersCollection(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                Offers = new List<Offer>();
                var reader = new JsonFx.Json.JsonReader();
                dynamic offers = reader.Read(response.Content);

                foreach (dynamic offer in offers)
                {
                    Offers.Add(new Offer(offer, response.StatusCode));
                }
            }
            else if (StatusCode == HttpStatusCode.NoContent)
            {
                Offers = new List<Offer>();
            }
            else
            {
                this.DeserializeErrors(response.Content);
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
