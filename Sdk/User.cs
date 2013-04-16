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
    public class User : ApiResponse
    {
        [JsonProperty ("id")]
        public Guid Id { get; set; }
        [JsonProperty ("firstName")]
        public string FirstName { get; set; }
        [JsonProperty ("lastName")]
        public string LastName { get; set; }
        [JsonProperty ("email")]
        public string Email { get; set; }
        [JsonProperty("accountBalances")]
        public List<UserAccountBalance> AccountBalances { get; set; }

        public User(IRestResponse response)
        {
            this.StatusCode = response.StatusCode;
            AccountBalances = new List<UserAccountBalance>();

            if (this.StatusCode == HttpStatusCode.OK)
            {
                JsonConvert.PopulateObject(response.Content, this);
            }
            else if (this.StatusCode == HttpStatusCode.NoContent)
            {
                //NOP:    
            }
            else
            {
                this.DeserializeErrors(response.Content);
            }
        }

        public static Task<UserToken> Authenticate(string userName, string password)
        {
            var request = ClientHelper.Request("users/authenticate", Method.POST);

            request.AddParameter("username", userName);
            request.AddParameter("password", password);

            var tcs = new TaskCompletionSource<UserToken>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(new UserToken(response));
                }
                else
                {
                    tcs.SetException(response.ErrorException);
                }
            });
            return tcs.Task;
        }

        public static Task<HttpStatusCode> SignOut(string token)
        {
            var request = ClientHelper.Request("users/signout", Method.DELETE);
            request.AddUrlSegment("token", token);

            var tcs = new TaskCompletionSource<HttpStatusCode>();
            ClientHelper.Client().ExecuteAsync(request, response =>
            {
                if (response.ErrorException == null)
                {
                    tcs.SetResult(response.StatusCode);
                }
                else
                {
                    tcs.SetException(response.ErrorException);
                }
            });
            return tcs.Task;
        }

        public static Task<User> Fetch(Guid userId, string userToken)
        {
            var request = ClientHelper.Request("users/{id}", Method.GET, userToken);
            request.AddUrlSegment("id", userId.ToString());

            var tcs = new TaskCompletionSource<User>();
            ClientHelper.Client().ExecuteAsync(request, response =>
                {
                    if (response.ErrorException == null)
                    {
                        tcs.SetResult(new User(response));
                    }
                    else
                    {
                        tcs.SetException(response.ErrorException);
                    }
                });
            return tcs.Task;
        }

    }
}
