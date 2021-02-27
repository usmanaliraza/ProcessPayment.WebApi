using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.EFCore.DataSeed;

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
