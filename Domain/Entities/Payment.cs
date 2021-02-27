using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
	public class Payment
	{
		public int Id { get; set; }
		public string CreditCardNumber { get; set; }
		public string CardHolder { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string SecurityCode { get; set; }
		public Decimal Amount { get; set; }
		public int PaymentStateId { get; set; }

		[ForeignKey("PaymentStateId")]
		public PaymentState PaymentState { get; set; }
	}
}
