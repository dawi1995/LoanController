using LoanControllerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static LoanControllerApp.Services.Web.ProxyClientCore;

namespace LoanControllerApp.Services.Web
{
    public class SecurityProxyClient
    {
        private string _token = string.Empty;
        private string controllerName = "security";
        public SecurityProxyClient(string token = null)
        {
            _token = token;
        }
        public async Task<LoginData> Login(User user)
        {
            try
            {
                ProxyClientCore client = new ProxyClientCore(_token);
                Dictionary<string, string> uriParams = new Dictionary<string, string>();
                string result = await client.SendRequest(HttpMethodType.Post, controllerName,
                "login", user, uriParams);

                if (!string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<LoginData>(result);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetAnonymousToken()
        {
            try
            {
                ProxyClientCore client = new ProxyClientCore(_token);
                Dictionary<string, string> uriParams = new Dictionary<string, string>();
                string result = await client.SendRequest(HttpMethodType.Get, controllerName,
                "getAnonymousToken", null, uriParams);

                if (!string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<string>(result);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
