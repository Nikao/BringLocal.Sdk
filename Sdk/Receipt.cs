using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Receipt : ApiResponse
    {
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public string MerchantName  { get; set; }
        public string OfferName { get; set; }
        public int FulfillmentTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public int FulfillmentStatusId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Receipt(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                var reader = new JsonFx.Json.JsonReader();
                dynamic receipt = reader.Read(response.Content);

                Id = new Guid(receipt.id);
                OfferId = new Guid(receipt.offerId);
                MerchantName = receipt.merchantName;
                OfferName = receipt.offerName;
                FulfillmentTypeId = receipt.fulfillmentTypeId;
                OrderStatusId = receipt.orderStatusId;
                FulfillmentStatusId = receipt.fulfillmentStatusId;
                ShippingName = receipt.shippingName;
                ShippingAddress1 = receipt.shippingAddress1;
                ShippingAddress2 = receipt.shippingAddress2;
                ShippingCity = receipt.shippingCity;
                ShippingState = receipt.shippingState;
                ShippingPostalCode = receipt.shippingPostalCode;
                PurchaseDate = receipt.purchaseDate;
                ExpirationDate = Convert.ToDateTime(receipt.expirationDate);
            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }
    }
}