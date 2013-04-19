using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Receipt : ApiResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("offerId")]
        public Guid OfferId { get; set; }
        [JsonProperty("merchantName")]
        public string MerchantName  { get; set; }
        [JsonProperty("offerName")]
        public string OfferName { get; set; }
        [JsonProperty("fulfillmentTypeId")]
        public int FulfillmentTypeId { get; set; }
        [JsonProperty("offerStatusId")]
        public int OrderStatusId { get; set; }
        [JsonProperty("fulfillmentStatusId")]
        public int FulfillmentStatusId { get; set; }
        [JsonProperty("referralUrl")]
        public string ReferralUrl { get; set; }
        [JsonProperty("shippingName")]
        public string ShippingName { get; set; }
        [JsonProperty("shippingAddress1")]
        public string ShippingAddress1 { get; set; }
        [JsonProperty("shippingAddress2")]
        public string ShippingAddress2 { get; set; }
        [JsonProperty("shippingCity")]
        public string ShippingCity { get; set; }
        [JsonProperty("shippingState")]
        public string ShippingState { get; set; }
        [JsonProperty("shippingPostalCode")]
        public string ShippingPostalCode { get; set; }
        [JsonProperty("purchaseDate")]
        public DateTime PurchaseDate { get; set; }
        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }

        public Receipt(IRestResponse response)
        {
            StatusCode = response.StatusCode;

            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                case HttpStatusCode.NoContent:
                case HttpStatusCode.InternalServerError:
                    //NOP no content to parse
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<Receipt> Fetch(Guid orderId, string userToken)
        {
            var request = ClientHelper.Request("orders/{id}", Method.GET, userToken);
            request.AddUrlSegment("id", orderId.ToString());

            var tcs = new TaskCompletionSource<Receipt>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new Receipt(response));
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