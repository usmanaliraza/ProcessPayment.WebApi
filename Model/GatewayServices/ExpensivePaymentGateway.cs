﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Polly;
using SharedComponent.Services;

namespace SharedComponent.GatewayServices
{
	public class ExpensivePaymentGateway : IGatewayService
	{
		public const int RetiresCount = 1;

		public async Task<bool> Post(decimal amount)
		{
			var returnResult = false;
			var httpClient = new HttpClient();
			var response = await Policy
				.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
				.WaitAndRetryAsync(RetiresCount, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
				{
				})
				.ExecuteAsync(() => httpClient.GetAsync("https://www.ExpensivePaymentGateway.com"));

			if (response.IsSuccessStatusCode)
			{
				returnResult = true;
			}
			return returnResult;
		}
	}
}
