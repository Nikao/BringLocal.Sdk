using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class User
    {

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
    }
}
