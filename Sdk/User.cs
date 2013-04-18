using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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

        public User()
        {
            AccountBalances = new List<UserAccountBalance>(); 
        }
        public User(IRestResponse response) : this()
        {
            StatusCode = response.StatusCode;
            
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    JsonConvert.PopulateObject(response.Content, this);
                    break;
                case HttpStatusCode.NoContent:
                    //NOP: nothing to deserialize
                    break;
                default:
                    DeserializeErrors(response.Content);
                    break;
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
            request.AddParameter("token", token);

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

        public static Task<ApiResponse> ChangePassword(Guid userId, Guid publisherId, string currentPassword, string newPassword,
                                                          string userToken)
        {
            var request = ClientHelper.Request("users/{id}/password", Method.PUT, userToken);
            request.AddUrlSegment("id", userId.ToString());
            request.AddParameter("publisherId", publisherId.ToString());
            request.AddParameter("currentPassword", currentPassword);
            request.AddParameter("newPassword", newPassword);

            var tcs = new TaskCompletionSource<ApiResponse>();
            ClientHelper.Client().ExecuteAsync(request, response =>
                {
                    if (response.ErrorException == null)
                    {
                        tcs.SetResult(new ApiResponse(response));
                    }
                    else
                    {
                        tcs.SetException(response.ErrorException);
                    }
                });
            return tcs.Task;
        }

        public static Task<ApiResponse> ResetPassword(Guid publisherId, Guid siteId, string email)
        {
            var request = ClientHelper.Request("users/resetpassword", Method.POST);
            request.AddParameter("publisherId", publisherId.ToString());
            request.AddParameter("siteId", siteId.ToString());
            request.AddParameter("email", email);

            var tcs = new TaskCompletionSource<ApiResponse>();
            ClientHelper.Client().ExecuteAsync(request, response =>
                {
                    if (response.ErrorException == null)
                    {
                        tcs.SetResult(new ApiResponse(response));
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