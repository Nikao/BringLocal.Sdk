using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

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
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
            //if (StatusCode == HttpStatusCode.OK)
            //{
            //    var reader = new JsonFx.Json.JsonReader();
            //    dynamic receipt = reader.Read(response.Content);

            //    Id = new Guid(receipt.id);
            //    OfferId = new Guid(receipt.offerId);
            //    MerchantName = receipt.merchantName;
            //    OfferName = receipt.offerName;
            //    FulfillmentTypeId = receipt.fulfillmentTypeId;
            //    OrderStatusId = receipt.orderStatusId;
            //    FulfillmentStatusId = receipt.fulfillmentStatusId;
            //    ShippingName = receipt.shippingName;
            //    ShippingAddress1 = receipt.shippingAddress1;
            //    ShippingAddress2 = receipt.shippingAddress2;
            //    ShippingCity = receipt.shippingCity;
            //    ShippingState = receipt.shippingState;
            //    ShippingPostalCode = receipt.shippingPostalCode;
            //    PurchaseDate = receipt.purchaseDate;
            //    ExpirationDate = Convert.ToDateTime(receipt.expirationDate);
            //}
            //else
            //{
            //    this.DeserializeErrors(response.Content);
            //}
        }
    }
}