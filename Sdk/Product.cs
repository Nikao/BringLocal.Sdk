using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal RealPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public int VoucherLimit { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int BundleSize { get; set; }
        public bool SoldOut { get; set; }
        
        public Product(dynamic item)
        {
            Id = item.id;
            Name = item.name;
            Description = item.description;
            RealPrice = Convert.ToDecimal(item.realPrice);
            Discount = Convert.ToDecimal(item.discount);
            Price = Convert.ToDecimal(item.price);
            VoucherLimit = item.voucherLimit;
            DiscountPercentage = Convert.ToDecimal(item.discountPercentage);
            BundleSize = item.bundleSize;
            SoldOut = item.soldOut;
        }
    }
}
