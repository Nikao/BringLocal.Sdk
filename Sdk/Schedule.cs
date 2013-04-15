using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class Schedule
    {
        public int Placement { get; set; }
        public Guid SiteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Schedule(dynamic item)
        {
            Placement = item.placement;
            SiteId = new Guid(item.siteId);
            StartDate = Convert.ToDateTime(item.startDate);
            EndDate = Convert.ToDateTime(item.endDate);
        }
    }
}