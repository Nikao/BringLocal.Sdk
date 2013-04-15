using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Publisher : ApiResponse
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<AcceptedCreditCard> AcceptedCreditCards { get; set; }

        private Publisher(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                var reader = new JsonFx.Json.JsonReader();
                dynamic publisher = reader.Read(response.Content);

                Initialize(publisher);
            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }

        internal Publisher(dynamic publisher, System.Net.HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            Initialize(publisher);
        }

        private void Initialize(dynamic item)
        {
            Name = item.name;
            Id = item.id;

            AcceptedCreditCards = new List<AcceptedCreditCard>();
            foreach (var acc in item.acceptedCreditCards)
            {
                AcceptedCreditCards.Add(new AcceptedCreditCard(acc));
            }
        }

        public static Task<Publisher> Fetch(string id)
        {
            var request = ClientHelper.Request("publishers/{id}", Method.GET);
            request.AddUrlSegment("id", id);

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
