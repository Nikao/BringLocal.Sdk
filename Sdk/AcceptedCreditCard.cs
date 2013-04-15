using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class AcceptedCreditCard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal AcceptedCreditCard(dynamic item)
        {
            Id = item.id;
            Name = item.name;
        }
    }
}
