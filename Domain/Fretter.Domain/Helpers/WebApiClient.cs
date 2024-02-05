using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Helpers
{
    public class OAuth2Token
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string username { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public long expires_in { get; set; }
    }

    public class WebApiClient
    {
        public OAuth2Token OAuth2Token { get; private set; }
        public bool IsAuthenticated
        {
            get { return OAuth2Token != null && OAuth2Token.access_token != null; }
        }

        private string UrlBase { get; set; }
        private string ApiKey { get; set; }
        public bool IsAnonymous { get; set; }
        public int? Timeout { get; set; }
        private Dictionary<string, string> Headers;

        public WebApiClient(string urlbase, int? timeout = null)
        {
            Timeout = timeout;
            UrlBase = urlbase;
            Headers = new Dictionary<string, string>();
        }
        public WebApiClient(string urlbase, string apiKey)
        {
            UrlBase = urlbase;
            ApiKey = apiKey;
            Headers = new Dictionary<string, string>();
        }
        public void AddHeader(string name, string value)
        {
            Headers.Add(name, value);
        }
        private HttpClient GetNewHttpClient(List<KeyValuePair<string, string>> headerData = null)
        {
            var httpClient = new HttpClient();
            if (Timeout != null)
                httpClient.Timeout = new TimeSpan(0, 0, (int)Timeout);
            httpClient.BaseAddress = new Uri(UrlBase);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (IsAuthenticated && string.IsNullOrEmpty(ApiKey))
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + OAuth2Token.access_token);
            }
            else if (!string.IsNullOrEmpty(ApiKey))
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");
                httpClient.DefaultRequestHeaders.Add("Authorization", ApiKey);
            }

            if (headerData != null)
            {
                foreach (var header in headerData)
                {
                    httpClient.DefaultRequestHeaders.Remove(header.Key);
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    httpClient.DefaultRequestHeaders.Remove(header.Key);
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            return httpClient;
        }
        public async Task<OAuth2Token> AuthenticateGrantType(string route, string userName, string password)
        {
            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("grant_type", "password"));
            body.Add(new KeyValuePair<string, string>("username", userName));
            body.Add(new KeyValuePair<string, string>("password", password));



            using (var HttpClient = GetNewHttpClient())
            {
                HttpResponseMessage response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(body));



                if (!response.IsSuccessStatusCode)
                    throw new Exception("Nome do usuário ou senha inválidos.");



                OAuth2Token = await response.Content.ReadAsAsync<OAuth2Token>();
            }



            return OAuth2Token;
        }
        public async Task<OAuth2Token> AuthenticateGrantTypeOAuth2(string route, string clientId, string clientSecret, string scope, string userName, string password)
        {
            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("grant_type", "password"));
            body.Add(new KeyValuePair<string, string>("username", userName));
            body.Add(new KeyValuePair<string, string>("password", password));
            body.Add(new KeyValuePair<string, string>("client_id", clientId));
            body.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            body.Add(new KeyValuePair<string, string>("scope", scope));

            using (var HttpClient = GetNewHttpClient())
            {
                if (HttpClient.BaseAddress.Scheme == "https")
                {
                    ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                HttpResponseMessage response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(body));

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Nome do usuário ou senha inválidos.");

                OAuth2Token = await response.Content.ReadAsAsync<OAuth2Token>();
            }

            return OAuth2Token;
        }

        public async Task<OAuth2Token> AuthenticateGrantTypeOAuth2(string route, List<KeyValuePair<string, string>> body)
        {
            using (var HttpClient = GetNewHttpClient())
            {
                if (HttpClient.BaseAddress.Scheme == "https")
                {
                    ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                HttpResponseMessage response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(body));

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Nome do usuário ou senha inválidos.");

                OAuth2Token = await response.Content.ReadAsAsync<OAuth2Token>();
            }

            return OAuth2Token;
        }

        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            return false;
        }

        public async Task<HttpResponseMessage> Post(string route, object data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);

                return response;
            }
        }

        public async Task<T> Post<T>(string route, object data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o POST");

                return default(T);
            }
        }
        public async Task<bool> PostFormData(string route, MultipartFormDataContent data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");



            using (var HttpClient = GetNewHttpClient())
            {
                HttpResponseMessage response = await HttpClient.PostAsync(route, data);



                return response.IsSuccessStatusCode;
            }
        }
        public async Task<List<T>> PostList<T>(string route, dynamic data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");
            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o POST");
                return default(List<T>);
            }
        }
        public async Task<HttpResponseMessage> PostListWithHeader<T>(string route, dynamic data, string arrayName, List<KeyValuePair<string, string>> header)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");



            using (var HttpClient = GetNewHttpClient(header))
            {
                string jsonData;
                var json = JsonConvert.SerializeObject(data);

                if (!string.IsNullOrEmpty(arrayName))
                    jsonData = @"{" + "\"" + arrayName + "\"" + ":" + json + "}";
                else jsonData = json;

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);



                return response;
            }
        }
        public async Task<T> PostWithHeader<T>(string route, dynamic data, string arrayName, List<KeyValuePair<string, string>> header)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient(header))
            {
                string jsonData;
                var json = JsonConvert.SerializeObject(data);

                if (!string.IsNullOrEmpty(arrayName))
                    jsonData = @"{" + "\"" + arrayName + "\"" + ":" + json + "}";
                else jsonData = json;

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);


                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o POST");
                string resultContent = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<T>(resultContent);
            }
        }
        public async Task<HttpResponseMessage> PostWithHeader<T>(string route, object data, List<KeyValuePair<string, string>> header = null)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(route, content);

                return response;
            }
        }
        public async Task<HttpResponseMessage> Put<T>(string route, T data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");
            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PutAsync(route, content);

                return response;
            }
        }
        public async Task<T> Delete<T>(string route)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");
            using (var HttpClient = GetNewHttpClient())
            {
                HttpResponseMessage response = await HttpClient.DeleteAsync(route);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o DELETE");
                return await response.Content.ReadAsAsync<T>();
            }
        }
        public async Task<T> Get<T>(string route)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");
            using (var HttpClient = GetNewHttpClient())
            {
                HttpResponseMessage response = await HttpClient.GetAsync(route);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o GET");

                var resultString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(resultString);
                //return await response.Content.ReadAsAsync<T>();
            }
        }
        public async Task<T> GetWithHeader<T>(string route, List<KeyValuePair<string, string>> header)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient(header))
            {
                HttpResponseMessage response = await HttpClient.GetAsync(route);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o GET");
                string resultContent = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<T>(resultContent);
            }
        }
        public async Task<T> GetWithHeader<T>(string route, string header)
        {
            return await GetWithHeader<T>(route, BuildHeader(header));
		}

        public async Task<HttpResponseMessage> GetWithResponse<T>(string route)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient())
            {

                HttpResponseMessage response = await HttpClient.GetAsync(route);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("WebApiClient - Erro ao realizar o GET");

                return response;
            }
        }
        public async Task<HttpResponseMessage> PutWithResponse<T>(string route, dynamic data)
        {
            if ((!IsAuthenticated && string.IsNullOrEmpty(ApiKey)) && (IsAnonymous = false))
                throw new AuthenticationException("Sessão inválida. Faça nova autenticação");

            using (var HttpClient = GetNewHttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PutAsync(route, content);

                return response;
            }
        }

		private List<KeyValuePair<string, string>> BuildHeader(string layoutHeader)
		{
			var listHeaderItem = new List<KeyValuePair<string, string>>();
			if (!string.IsNullOrEmpty(layoutHeader))
			{
				var listHeader = layoutHeader.Split("|").ToList();
				foreach (var itemHeader in listHeader)
				{
					if (itemHeader.Contains("="))
					{
						var listHeaderValue = itemHeader.Split("=").ToList();
						listHeaderItem.Add(new KeyValuePair<string, string>(listHeaderValue[0], listHeaderValue[1]));
					}
				}
			}
			return listHeaderItem;
		}
    }
}
