using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
	public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
	{
		public PaymentRepository(ApplicationContext context) : base(context)
		{
		}
	}
}
