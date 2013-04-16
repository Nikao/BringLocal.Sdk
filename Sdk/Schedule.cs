using Newtonsoft.Json;
using System;

namespace BringLocal.Sdk
{
    public class Schedule
    {
        [JsonProperty("placement")]
        public int Placement { get; set; }
        [JsonProperty("siteId")]
        public Guid SiteId { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }
}