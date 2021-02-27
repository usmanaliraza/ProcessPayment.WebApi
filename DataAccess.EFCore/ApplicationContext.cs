using DataAccess.EFCore.DataSeed;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PaymentStateConfiguration());
		}
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentState> PaymentStates { get; set; }
	}
}
