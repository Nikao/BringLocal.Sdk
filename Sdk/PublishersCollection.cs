using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class PublishersCollection : ApiResponse
    {
        public List<Publisher> Publishers { get; set; } 
        private PublishersCollection(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                Publishers = new List<Publisher>();
                var reader = new JsonFx.Json.JsonReader();
                dynamic publishers = reader.Read(response.Content);

                foreach (dynamic publisher in publishers)
                {
                    Publishers.Add(new Publisher(publisher, response.StatusCode));
                }
            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }

        public static Task<PublishersCollection> Fetch()
        {
            var request = ClientHelper.Request("publishers", Method.GET);

            var tcs = new TaskCompletionSource<PublishersCollection>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new PublishersCollection(response));
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
