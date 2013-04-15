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
        public List<CreditCard> CreditCards { get; set; }

        private CreditCardCollection(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            CreditCards = new List<CreditCard>();
            if (StatusCode == HttpStatusCode.OK)
            {
                var reader = new JsonFx.Json.JsonReader();
                dynamic creditCards = reader.Read(response.Content);

                foreach (dynamic creditCard in creditCards)
                {
                    CreditCards.Add(new CreditCard(creditCard));
                }
            }
            else if (StatusCode == HttpStatusCode.NoContent)
            {}
            else
            {
                this.DeserializeErrors(response.Content);
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
