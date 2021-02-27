﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccess.EFCore.DataSeed
{
	public static class MigrationManager
	{
		public static IHost MigrateDatabase(this IHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
				{
					try
					{
						appContext.Database.Migrate();
					}
					catch (Exception ex)
					{
						//Log errors or do anything you think it's needed
						throw;
					}
				}
			}
			return host;
		}
	}
}
