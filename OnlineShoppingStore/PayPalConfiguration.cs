using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore
{
    public class PayPalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PayPalConfiguration()
        {
            var Config = GetConfig();
            ClientId = "AZFy0WBOCiH_ynbtNlAieBPn3VjsG-LiOCsJGQQ69viKBzQIs4UNWs-NuUngeFwlVRgqfi6NJ-4rVOuC";
            ClientSecret = "EDl-0ew0KkT08BGf84j5NRdp0ljMwcplxXiq-Ki9FS1GsdFJNhBbULTTjxe4mZn-XCb72JSYoTL0HLzW";
        }

        // To get properties from the web.config

        private static Dictionary<string,string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        // To get accesstocken from paypal
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // Return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}