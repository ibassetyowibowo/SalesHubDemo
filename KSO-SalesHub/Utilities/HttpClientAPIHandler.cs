using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace KSO_SalesHub.Utilities
{
	public class HttpClientAPIHandler
	{
        public HttpClientAPIHandler()
        {
                
        }


		public async Task<(bool Status, string Msg, T Data)> PostDataAPIUrl<T>(string Url, string Api, string Content)
		{
			try
			{
				var socketsHandler = new SocketsHttpHandler
				{
					PooledConnectionLifetime = TimeSpan.FromMinutes(60),
					PooledConnectionIdleTimeout = TimeSpan.FromMinutes(60),
					MaxConnectionsPerServer = 500,

					SslOptions = new SslClientAuthenticationOptions
					{
						RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
					}
				};

				using (var client = new HttpClient(socketsHandler) { BaseAddress = new Uri(Url) })
				{
					var JsonContent = JsonConvert.DeserializeObject<T>(Content);

					string serailizeddto = JsonConvert.SerializeObject(JsonContent, new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver()
					});

					var inputMessage = new HttpRequestMessage
					{
						Content = new StringContent(serailizeddto.Replace("\\\\", ""), Encoding.UTF8, "application/json")
					};

					inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage message = await client.PostAsync(Api, inputMessage.Content);


					var strResponse = await message.Content.ReadAsStringAsync();
					var objResponse = JsonConvert.DeserializeObject<T>(strResponse);

					return (true, "Success", objResponse);
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message, JsonConvert.DeserializeObject<T>(null));
			}
		}

	}
}
