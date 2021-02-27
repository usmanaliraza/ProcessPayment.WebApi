using System;

namespace Model.Dto
{
	public class PaymentDto
	{
		public string CreditCardNumber { get; set; }
		public string CardHolder { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string SecurityCode { get; set; }
		public Decimal Amount { get; set; }
	}
}
