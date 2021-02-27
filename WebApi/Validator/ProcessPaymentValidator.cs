using FluentValidation;
using Model.Dto;
using System;

namespace WebApi.Validator
{
	public class ProcessPaymentValidator : AbstractValidator<PaymentDto>
	{
		public ProcessPaymentValidator()
		{
			RuleFor(p => p.CreditCardNumber)
				.NotEmpty().WithMessage("{PropertyName} should be not empty.");

			RuleFor(p => p.CardHolder)
				.NotEmpty().WithMessage("{PropertyName} should be not empty. ");

			RuleFor(p => p.ExpirationDate)
				.NotEmpty().WithMessage("{PropertyName} should be not empty. ").
				GreaterThanOrEqualTo(p => DateTime.Now).WithMessage("Expiration cannot be past");

			RuleFor(x => x.SecurityCode)
				.Must(x => x == null || x.Length == 3).WithMessage("Security card length must be 3 or null");

			RuleFor(p => p.Amount)
				.NotEmpty().WithMessage("{PropertyName} should be not empty. ")
				.GreaterThan(0)
				.WithMessage("Must be a positive value");
		}

	}

}
