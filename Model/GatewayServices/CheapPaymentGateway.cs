using Polly;
using SharedComponent.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharedComponent.GatewayServices
{
	public class CheapPaymentGateway : IGatewayService
	{
		public async Task<bool> Post(decimal amount)
		{
			var returnResult = false;
			var httpClient = new HttpClient();
			var response = await Policy
				.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
				.WaitAndRetryAsync(0, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
				{
				})
				.ExecuteAsync(() => httpClient.GetAsync("https://www.CheapPaymentGateway.com"));

			if (response.IsSuccessStatusCode)
			{
				returnResult = true;
			}
			return returnResult;
		}
	}
}

