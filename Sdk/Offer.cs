using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace BringLocal.Sdk
{
    public class Offer : ApiResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("offerName")]
        public string OfferName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("finePrint")]
        public string FinePrint { get; set; }
        [JsonProperty("specialInstructions")]
        public string SpecialInstructions { get; set; }
        [JsonProperty("quantitySold")]
        public int QuantitySold { get; set; }
        [JsonProperty("offerUrl")]
        public string OfferUrl { get; set; }
        [JsonProperty("merchantName")]
        public string MerchantName { get; set; }
        [JsonProperty("requiresLocation")]
        public bool RequiresLocation { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("fulfillmentTypeId")]
        public int FulfillmentTypeId { get; set; }
        [JsonProperty("requireShippingAddress")]
        public bool RequireShippingAddress { get; set; }
        [JsonProperty("redemptionStartDate")]
        public DateTime? RedemptionStartDate { get; set; }
        [JsonProperty("redemptionEndDate")]
        public DateTime? RedemptionEndDate { get; set; }
        [JsonProperty("highlights")]
        public List<string> HighLights { get; set; }
        [JsonProperty("images")]
        public List<OfferImages> Images { get; set; }
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
        [JsonProperty("locations")]
        public List<Location> Locations { get; set; }
        [JsonProperty("schedules")]
        public List<Schedule> Schedules { get; set; } 

        public Offer()
        {
            HighLights = new List<string>();
            Images = new List<OfferImages>();
            Products = new List<Product>();
            Locations = new List<Location>();
            Schedules = new List<Schedule>();
        }
        public Offer(IRestResponse response) : this()
        {
            StatusCode = response.StatusCode;

            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP: nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        public static Task<Offer> Fetch(Guid offerId)
        {
            var request = ClientHelper.Request("offers", Method.GET);
            request.AddParameter("id", offerId);
            
            var tcs = new TaskCompletionSource<Offer>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new Offer(response));
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