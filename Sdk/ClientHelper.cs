using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public static class ClientHelper
    {
        public static IRestClient Client()
        {
            return new RestClient(SdkSettings.ApiEndPointUrl);
        }
        public static IRestRequest Request(string uri, Method method)
        {
            var request = new RestRequest(uri, method);
            request.AddHeader("X-BringLocal-APIKey", SdkSettings.ApiKey);
            return request;
        }

        public static IRestRequest Request(string uri, Method method, string userToken)
        {
            var request = Request(uri, method);
            request.AddHeader("Authorization", "Bearer " + userToken);
            return request;
        }
    }
}