using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Guid? CreditCardReferenceId { get; set; }
        public bool? SaveCreditCard { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CreditCardType { get; set; }
        public string CreditCardCVV { get; set; }
        public string CreditCardExpirationMonth { get; set; }
        public string CreditCardExpirationYear { get; set; }
        [JsonProperty ("cardHolderName")]
        public string CreditCardHolderName { get; set; }
        [JsonProperty("address")]
        public string CreditCardHolderAddress { get; set; }
        [JsonProperty("zip")]
        public string CreditCardHolderZip { get; set; }
        public string CouponCode { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }

        public Task<Receipt> Submit(string userToken)
        {
            var request = ClientHelper.Request("orders", Method.POST, userToken);
            //request.AddParameter("siteId", SiteId);
            //request.AddParameter("offerId", OfferId);
            //request.AddParameter("agreeToTerms", AgreeToTerms);
            ////request.AddParameter("products", Products);
            //foreach (var product in Products)
            //{         
            //    request.AddObject(product);
            //}

            //if (LocationId.HasValue) request.AddParameter("locationId", LocationId.Value);
            //if (CreditCardReferenceId.HasValue)
            //    request.AddParameter("creditCardReferenceId", CreditCardReferenceId.Value);
            //else
            //{
            //    request.AddParameter("creditCardNumber", CreditCardNumber ?? "");
            //    if(CreditCardType.HasValue) request.AddParameter("CreditCardType", CreditCardType.Value);
            //    request.AddParameter("creditCardCVV", CreditCardCVV ?? "");
            //    request.AddParameter("creditCardExpirationMonth", CreditCardExpirationMonth ?? "");
            //    request.AddParameter("creditCardExpirationYear", CreditCardExpirationYear ?? "");
            //    request.AddParameter("cardHolderName", CreditCardHolderName ?? "");
            //    request.AddParameter("address", CreditCardHolderAddress ?? "");
            //    request.AddParameter("zip", CreditCardHolderZip ?? "");
            //    if(SaveCreditCard.HasValue) request.AddParameter("saveCreditCard", SaveCreditCard.Value);
            //}
            //request.AddParameter("couponCode", CouponCode ?? "");
            //request.AddParameter("shippingName", ShippingName ?? "");
            //request.AddParameter("shippingAddress1", ShippingAddress1 ?? "");
            //request.AddParameter("shippingAddress2", ShippingAddress2 ?? "");
            //request.AddParameter("shippingCity", ShippingCity ?? "");
            //request.AddParameter("shippingState", ShippingState ?? "");
            //request.AddParameter("shippingPostalCode", ShippingPostalCode ?? "");

            var json = JsonConvert.SerializeObject(this);
            Console.WriteLine("My JSON");
            Console.WriteLine(json);
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

