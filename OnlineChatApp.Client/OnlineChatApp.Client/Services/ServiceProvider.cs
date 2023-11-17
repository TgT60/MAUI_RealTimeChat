using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineChatApp.Client.Services.Authenticate;

namespace OnlineChatApp.Client.Services
{
	public class ServiceProvider
	{
		private static ServiceProvider _instance;

		private string _serverRootUrl = "https://10.0.2.2:7082";

		private string _accessToken = "";

        private ServiceProvider() {}

		public static ServiceProvider GetInstance()
		{
			if (_instance == null)
			{ 
				_instance = new ServiceProvider();
			}

			return _instance;
		}

		public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request) 
		{ 
			using (HttpClient client = new HttpClient()) 
			{
				var httpRequestMessage = new HttpRequestMessage();
				httpRequestMessage.Method = HttpMethod.Post;
				httpRequestMessage.RequestUri = new Uri(_serverRootUrl + "/Authenticate/Authenticate");

				if (request != null)
				{
					string jsonContent = JsonConvert.SerializeObject(request);
					var httpContent = new StringContent(jsonContent,encoding: Encoding.UTF8, "application/json");
					httpRequestMessage.Content = httpContent;
				}

				try 
				{
					var response = await client.SendAsync(httpRequestMessage);
					var responseContent = await response.Content.ReadAsStringAsync();

					var result = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);
					result.StatusCode = (int)response.StatusCode;
					result.StatusMessage = response.RequestMessage;

					if (result.StatusCode == 200) 
					{
						_accessToken = result.Token;
					}
				}
				catch (Exception ex)
				{ 
				
				}
			}
		}
	}
}
