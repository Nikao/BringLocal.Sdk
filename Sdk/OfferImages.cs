using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class OfferImages
    {
        public string Url { get; set; }
        public string Size { get; set; }

        public OfferImages(dynamic item)
        {
            Url = item.url;
            Size = item.size;
        }
    }
}
