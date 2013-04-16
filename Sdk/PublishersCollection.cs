using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class PublishersCollection : ApiResponse
    {
        [JsonProperty("publishers")]
        public List<Publisher> Publishers { get; set; } 
        public PublishersCollection(IRestResponse response)
        {
            Publishers = new List<Publisher>();
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, Publishers);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP - nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
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