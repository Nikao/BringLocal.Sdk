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
        public User(IRestResponse response)
        {
            this.StatusCode = response.StatusCode;

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
    }
}
