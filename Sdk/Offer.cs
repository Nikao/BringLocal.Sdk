using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BringLocal.Sdk
{
    public class Offer : ApiResponse
    {
        public Guid Id { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public string FinePrint { get; set; }
        public string SpecialInstructions { get; set; }
        public int QuantitySold { get; set; }
        public string OfferUrl { get; set; }
        public string MerchantName { get; set; }
        public bool RequiresLocation { get; set; }
        public bool Active { get; set; }
        public int FulfillmentTypeId { get; set; }
        public bool RequireShippingAddress { get; set; }
        public DateTime RedemptionStartDate { get; set; }
        public DateTime RedemptionEndDate { get; set; }
        public List<string> HighLights { get; set; }
        public List<OfferImages> Images { get; set; }
        public List<Product> Products { get; set; }
        public List<Location> Locations { get; set; }
        public List<Schedule> Schedules { get; set; } 

        private Offer(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                var reader = new JsonFx.Json.JsonReader();
                dynamic publisher = reader.Read(response.Content);

                Initialize(publisher);
            }
            else if (StatusCode == HttpStatusCode.NoContent)
            {

            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }
        internal Offer(dynamic item, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Initialize(item);
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

        private void Initialize(dynamic item)
        {
            Id = new Guid(item.id);
            OfferName = item.offerName;
            Description = item.description;
            FinePrint = item.finePrint;
            SpecialInstructions = item.specialInstructions;
            QuantitySold = item.quantitySold;
            OfferUrl = item.offerUrl;
            MerchantName = item.merchantName;
            RequiresLocation = item.requiresLocation;
            Active = item.active;
            FulfillmentTypeId = item.fulfillmentTypeId;
            RequireShippingAddress = item.requireShippingAddress;
            RedemptionStartDate = Convert.ToDateTime(item.redemptionStartDate);
            RedemptionEndDate = Convert.ToDateTime(item.redemptionEndDate);
            HighLights = new List<string>();
            foreach (var highlight in item.highlights)
            {
                HighLights.Add(highlight);
            }
            Images = new List<OfferImages>();
            foreach (var image in item.images)
            {
                Images.Add(new OfferImages(image));
            }
            Products = new List<Product>();
            foreach (var product in item.products)
            {
                Products.Add(new Product(product));
            }
            Locations = new List<Location>();
            foreach (var location in item.locations)
            {
                Locations.Add(new Location(location));
            }
            Schedules = new List<Schedule>();
            foreach (var schedule in item.schedules)
            {
                Schedules.Add(new Schedule(schedule));
            }
        }
    }
}