using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Purchase
    {
        [JsonProperty("siteId")]
        public Guid SiteId { get; set; }
        [JsonProperty("offerId")]
        public Guid OfferId { get; set; }
        [JsonProperty("agreeToTerms")]
        public bool AgreeToTerms { get; set; }
        [JsonProperty("products")]
        public List<PurchaseProduct> Products { get; set; }
        [JsonProperty("locationId")]
        public Guid? LocationId { get; set; }
        [JsonProperty("creditCardReferenceId")]
        public Guid? CreditCardReferenceId { get; set; }
        [JsonProperty("saveCreditCard")]
        public bool? SaveCreditCard { get; set; }
        [JsonProperty("creditCardNumber")]
        public string CreditCardNumber { get; set; }
        [JsonProperty("creditCardType")]
        public int? CreditCardType { get; set; }
        [JsonProperty("creditCardCVV")]
        public string CreditCardCVV { get; set; }
        [JsonProperty("creditCardExpirationMonth")]
        public string CreditCardExpirationMonth { get; set; }
        [JsonProperty("creditCardExpirationYear")]
        public string CreditCardExpirationYear { get; set; }
        [JsonProperty ("cardHolderName")]
        public string CreditCardHolderName { get; set; }
        [JsonProperty("address")]
        public string CreditCardHolderAddress { get; set; }
        [JsonProperty("zip")]
        public string CreditCardHolderZip { get; set; }
        [JsonProperty("couponCode")]
        public string CouponCode { get; set; }
        [JsonProperty("spendCreditsOnPurchase")]
        public bool SpendCreditsOnPurchase { get; set; }
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
        [JsonProperty("referredById")]
        public Guid ReferredById { get; set; }

        public Purchase()
        {
            ReferredById = Guid.Empty;
        }

        public Task<Receipt> Submit(string userToken)
        {
            var request = ClientHelper.Request("orders", Method.POST, userToken);
            var json = JsonConvert.SerializeObject(this);
            request.AddParameter("text/json", JsonConvert.SerializeObject(this), ParameterType.RequestBody);
            
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