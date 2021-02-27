using System;

namespace Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IPaymentRepository Payments { get; }
		int Complete();
	}
}
