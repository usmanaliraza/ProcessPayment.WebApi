﻿using Polly;
using SharedComponent.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharedComponent.GatewayServices
{
	public class PremiumPaymentGateway : IGatewayService
	{

		public const int RetiresCount = 3;
		public async Task<bool> Post(decimal amount)
		{
			var returnResult = false;
			var httpClient = new HttpClient();
			var response = await Policy
				.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
				.WaitAndRetryAsync(RetiresCount, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
				{
				})
				.ExecuteAsync(() => httpClient.GetAsync("https://www.PremiumPaymentGateway.com"));

			if (response.IsSuccessStatusCode)
			{
				returnResult = true;
			}
			return returnResult;
		}
	}
}

