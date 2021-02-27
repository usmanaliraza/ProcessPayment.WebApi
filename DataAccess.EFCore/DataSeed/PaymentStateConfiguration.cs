using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EFCore.DataSeed
{
	public class PaymentStateConfiguration : IEntityTypeConfiguration<PaymentState>
	{
		public void Configure(EntityTypeBuilder<PaymentState> builder)
		{
			builder.ToTable("PaymentStates");
			builder.HasData
			(
				new PaymentState
				{
					Id = 1,
					Description = "Pending"
				},
				new PaymentState
				{
					Id = 2,
					Description = "Processed"
				},
				new PaymentState
				{
					Id = 3,
					Description = "Failed"
				}
			);
		}
	}
}
