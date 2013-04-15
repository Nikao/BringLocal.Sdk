using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Location(dynamic item)
        {
            Id = new Guid(item.id);
            State = item.street;
            City = item.city;
            State = item.state;
            Zip = item.zip;
        }
    }
}
