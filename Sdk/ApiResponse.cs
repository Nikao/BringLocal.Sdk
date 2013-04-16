using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class ApiResponse
    {
        [JsonIgnore]
        public System.Net.HttpStatusCode StatusCode;
        [JsonProperty("errors")]
        public List<ApiError> ApiErrors { get; set; }

        protected void DeserializeErrors(string content)
        {
            ApiErrors = new List<ApiError>();
            JsonConvert.PopulateObject(content, ApiErrors);
        }
    }
}
