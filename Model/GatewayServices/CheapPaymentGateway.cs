using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedComponent.Services;

namespace SharedComponent.GatewayServices
{
	public class CheapPaymentGateway : IGatewayService
	{

		public async Task<bool> Post(decimal amount)
		{
			// call 3rd service at this point to get required result
			return await Task.FromResult(true);
		}
	}
}
