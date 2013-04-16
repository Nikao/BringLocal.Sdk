using Newtonsoft.Json;
using System;

namespace BringLocal.Sdk
{
    public class Product
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("realPrice")]
        public decimal RealPrice { get; set; }
        [JsonProperty("discount")]
        public decimal Discount { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("voucherLimit")]
        public int VoucherLimit { get; set; }
        [JsonProperty("discountPercentage")]
        public decimal DiscountPercentage { get; set; }
        [JsonProperty("bundleSize")]
        public int BundleSize { get; set; }
        [JsonProperty("soldOut")]
        public bool SoldOut { get; set; }
    }
}
