using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringLocal.Sdk
{
    public static class SdkSettings
    {
        public static string ApiEndPointUrlAppSettingKey = "BringLocal.ApiEndPointUrl";
        public static string ApiKeyAppSettingKey = "BringLocal.ApiKey";
        static SdkSettings()
        {
            if (ConfigurationManager.AppSettings[ApiEndPointUrlAppSettingKey] != null)
            {
                ApiEndPointUrl = ConfigurationManager.AppSettings[ApiEndPointUrlAppSettingKey];
            }
            if (ConfigurationManager.AppSettings[ApiKeyAppSettingKey] != null)
            {
                ApiKey = ConfigurationManager.AppSettings[ApiKeyAppSettingKey];
            }
        }

        public static string ApiEndPointUrl { get; set; }
        public static string ApiKey { get; set; }
    }
}
