using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class ApiResponse
    {
        public System.Net.HttpStatusCode StatusCode;
        public List<ApiError> ApiErrors { get; set; }

        protected void DeserializeErrors(string content)
        {
            var reader = new JsonFx.Json.JsonReader();

            ApiErrors = new List<ApiError>();
            dynamic errors = reader.Read(content);
            foreach (dynamic error in errors)
            {
                ApiErrors.Add(new ApiError{ErrorCode = error.errorCode, Message = error.message});
            }
        }
    }
}
