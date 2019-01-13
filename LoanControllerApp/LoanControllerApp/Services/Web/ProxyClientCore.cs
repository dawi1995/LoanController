using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoanControllerApp.Utils;


namespace LoanControllerApp.Services.Web
{
    public class ProxyClientCore
    {
        private HttpClient _httpClient = null;
        private string _token = null;
        private readonly CookieContainer _cookieContainer;
        private string _secondUrl = null;
        private string _oAuthType = "Basic";
        public ProxyClientCore()
        {
            _httpClient = new HttpClient();
            _cookieContainer = new CookieContainer();

            var messageHandler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer
            };

            _httpClient = new HttpClient(messageHandler);
        }

        public ProxyClientCore(string token, string secondUrl = null, string oAuthType = "Bearer")
            : this()
        {
            _token = token;
            _secondUrl = secondUrl;
            _oAuthType = oAuthType;

            if (!string.IsNullOrEmpty(secondUrl))
            {
                _httpClient = new HttpClient();
                _cookieContainer = new CookieContainer();

                var messageHandler = new HttpClientHandler
                {
                    CookieContainer = _cookieContainer,
                    AllowAutoRedirect = false
                };

                _httpClient = new HttpClient(messageHandler);
            }
        }

        public async Task<string> SendRequest(HttpMethodType httpMethod, string controller, string action, Dictionary<string, string> urlParams)
        {
            return await SendRequest(httpMethod, controller, action, null, urlParams);
        }

        public async Task<string> SendRequest(HttpMethodType httpMethod, string controller, string action, object body, Dictionary<string, string> urlParams)
        {
            if (!string.IsNullOrEmpty(_token))
                AddAuthorizationHeader(_token);

            string url = CreateFullUrl(controller, action, urlParams);

            switch (httpMethod)
            {
                case HttpMethodType.Get: return await Get(url);
                case HttpMethodType.Post: return await Post(body, url);
                case HttpMethodType.Put: return await Put(body, url);
                case HttpMethodType.Delete: return await Delete(url);
                default: return await Get(url);
            }
        }

        private string CreateFullUrl(string controller, string action, Dictionary<string, string> urlParams)
        {
            string url = string.Format("{0}/{1}", string.IsNullOrEmpty(_secondUrl) ? Consts.WEB_API_URL : _secondUrl, controller);
            if (!string.IsNullOrEmpty(action))
                url += string.Format("/{0}", action);
            url += ConvertUrlParams(urlParams);

            return url;
        }

        private async Task<string> Get(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> Put(object body, string url)
        {
            if (body.GetType().Equals(typeof(string)))
            {
                var response = await _httpClient.PutAsync(url, new StringContent((string)body));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var response = await _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> Post(object body, string url)
        {
            if (body == null)
            {
                var response = await _httpClient.PostAsync(url, new StringContent(""));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            else if (body.GetType().Equals(typeof(string)))
            {
                var response = await _httpClient.PostAsync(url, new StringContent((string)body, Encoding.UTF8, "application/x-www-form-urlencoded"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var response = await _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                if (string.IsNullOrEmpty(_secondUrl))
                {
                    response.EnsureSuccessStatusCode();
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> Delete(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private string ConvertUrlParams(Dictionary<string, string> urlParams)
        {
            if (urlParams == null)
                return string.Empty;

            List<string> list = new List<string>();

            string idValue = null;
            if (urlParams.ContainsKey("id"))
            {
                idValue = urlParams["id"];
                urlParams.Remove("id");
            }

            foreach (var item in urlParams)
            {
                list.Add(string.Format("{0}={1}", item.Key, item.Value));
            }

            string result = string.IsNullOrEmpty(idValue) ? "" : string.Format("/{0}", idValue);
            if (list.Count > 0)
            {
                result += string.Format("?{0}", string.Join("&", list));
            }

            return result;
        }

        protected void AddAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Remove(HttpHeaders.Authorization);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaders.Authorization, string.Format("{1} {0}", token, _oAuthType));
        }

        internal class HttpHeaders
        {
            public const string ContentType = "Content-Type";
            public const string AcceptEncoding = "Accept-Encoding";
            public const string Accept = "Accept";
            public const string AcceptLanguage = "Accept-Language";
            public const string UserAgent = "User-Agent";
            public const string Authorization = "Authorization";
        }

        internal class NonStandardHttpHeaders
        {
            public const string MethodOverride = "X-HTTP-Method-Override";
        }

        public enum HttpMethodType
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
