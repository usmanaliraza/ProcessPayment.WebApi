namespace SharedComponent.Enum
{
	public class Enum
	{
		public enum PaymentState
		{
			Pending = 1,
			Processed = 2,
			Failure = 3
		}

		public enum PaymentGatewayService
		{
			Cheap = 1,
			Expensive = 2,
			Premium = 3
		}
	}
}
