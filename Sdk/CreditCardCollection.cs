using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class CreditCardCollection : ApiResponse
    {
        [JsonProperty("creditCards")]
        public List<CreditCard> CreditCards { get; set; }

        private CreditCardCollection(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            CreditCards = new List<CreditCard>();

            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, CreditCards);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP : nothing to deserialize
                    break;
                default:
                    this.DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<CreditCardCollection> Fetch(Guid userId, string userToken)
        {
            var request = ClientHelper.Request("users/{id}/creditcards", Method.GET, userToken);
            request.AddUrlSegment("id", userId.ToString());
            
            var tcs = new TaskCompletionSource<CreditCardCollection>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new CreditCardCollection(response));
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
