using Polly;
using SharedComponent.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharedComponent.GatewayServices
{
	public class ExpensivePaymentGateway : IGatewayService
	{
		public const int RetiresCount = 1;

		public async Task<bool> Post(decimal amount)
		{
			//Make this value false in case of real scenario
			var returnResult = true;

			//Intentionally commented the code so it will b a success. as url is invalid
			
			//var httpClient = new HttpClient();
			//var response = await Policy
			//	.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
			//	.WaitAndRetryAsync(RetiresCount, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
			//	{
			//	})
			//	.ExecuteAsync(() => httpClient.GetAsync("https://www.ExpensivePaymentGateway.com"));

			//if (response.IsSuccessStatusCode)
			//{
			//	returnResult = true;
			//}
			return returnResult;
		}
	}
}
