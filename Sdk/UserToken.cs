using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BringLocal.Sdk
{
    public class UserToken : ApiResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }

        internal UserToken(IRestResponse response)
        {
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
            {
                var reader = new JsonFx.Json.JsonReader();
                dynamic token = reader.Read(response.Content);
                Id = new Guid(token.id);
                Token = token.token;
                Expires = token.expires;
            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }
    }
}
