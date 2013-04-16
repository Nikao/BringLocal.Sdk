using System.Net;
using Newtonsoft.Json;
using RestSharp;
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

        public ApiResponse()
        {
            ApiErrors = new List<ApiError>();
        }

        public ApiResponse(IRestResponse response) : this()
        {
            StatusCode = response.StatusCode;
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.NoContent:
                    //NOP- Nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
            }
        }

        protected void DeserializeErrors(string content)
        {
            if (ApiErrors == null) ApiErrors = new List<ApiError>();
            JsonConvert.PopulateObject(content, ApiErrors);
        }
    }
}
