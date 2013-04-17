using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class SitesCollection : ApiResponse
    {
        [JsonProperty("sites")]
        public List<Site> Sites { get; set; }
        public SitesCollection(IRestResponse response)
        {
            Sites = new List<Site>();
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, Sites);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP - nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<SitesCollection> Fetch()
        {
            var request = ClientHelper.Request("sites", Method.GET);

            var tcs = new TaskCompletionSource<SitesCollection>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new SitesCollection(response));
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