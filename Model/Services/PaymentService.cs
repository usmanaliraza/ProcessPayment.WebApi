using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponent.Services
{
	public interface IPaymentService
	{
		Task<bool> ProcessPaymentAsync(decimal amount);
	}

	public class PaymentService : IPaymentService
	{
		private readonly Func<Enum.Enum.PaymentGatewayService, IGatewayService> _gatewayServiceDelegate;

		public PaymentService(Func<Enum.Enum.PaymentGatewayService, IGatewayService> gatewayServiceDelegate)
		{
			_gatewayServiceDelegate = gatewayServiceDelegate;
		}
		public async Task<bool> ProcessPaymentAsync(decimal amount)
		{
			var returnedEnumValue = GatewayToCall(amount);

			var result = await _gatewayServiceDelegate(returnedEnumValue).Post(amount);

			return result;
		}

		private Enum.Enum.PaymentGatewayService GatewayToCall(decimal amount)
		{
			if (amount <= 20)
			{
				return Enum.Enum.PaymentGatewayService.Cheap;
			}

			else if (amount > 20 && amount <= 500)
			{
				return Enum.Enum.PaymentGatewayService.Expensive;
			}
			else
			{
				return Enum.Enum.PaymentGatewayService.Premium;
			}
		}
	}
}
