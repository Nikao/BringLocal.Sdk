using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public class NewUser
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("subscribe")]
        public bool Subscribe { get; set; }
        [JsonProperty("publisherId")]
        public Guid PublisherId { get; set; }

        public Task<User> Create()
        {
            var request = ClientHelper.Request("users", Method.POST);
            
            var json = JsonConvert.SerializeObject(this);
            Console.WriteLine("My JSON");
            Console.WriteLine(json);
            request.AddParameter("text/json", json, ParameterType.RequestBody);

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
