using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Subscribe
    {
        [JsonProperty("publisherId")]
        public Guid PublisherId { get; set; }
        [JsonProperty("siteId")]
        public Guid SiteId { get; set; }
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }

        public Task<Subscription> Submit()
        {
            var request = ClientHelper.Request("subscribe", Method.POST);
            var json = JsonConvert.SerializeObject(this);
            request.AddParameter("text/json", JsonConvert.SerializeObject(this), ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<Subscription>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new Subscription(response));
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
