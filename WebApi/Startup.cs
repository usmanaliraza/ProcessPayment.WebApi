using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedComponent.GatewayServices;
using SharedComponent.Services;
using System;
using System.Collections.Generic;
using Enum = SharedComponent.Enum.Enum;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddFluentValidation(s =>
				{
					s.RegisterValidatorsFromAssemblyContaining<Startup>();
					s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
				});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddDbContext<ApplicationContext>(options =>
			options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

			#region Repositories
			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<IPaymentRepository, PaymentRepository>();
			#endregion

			services.AddTransient<CheapPaymentGateway>();
			services.AddTransient<ExpensivePaymentGateway>();
			services.AddTransient<PremiumPaymentGateway>();
			services.AddTransient<Func<Enum.PaymentGatewayService, IGatewayService>>
			(serviceProvider => key =>
			{
				switch (key)
				{
					case Enum.PaymentGatewayService.Cheap:
						return serviceProvider.GetRequiredService<CheapPaymentGateway>();
					case Enum.PaymentGatewayService.Expensive:
						return serviceProvider.GetRequiredService<ExpensivePaymentGateway>();
					case Enum.PaymentGatewayService.Premium:
						return serviceProvider.GetRequiredService<PremiumPaymentGateway>();
					default:
						throw new KeyNotFoundException();
				}
			});
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPaymentService, PaymentService>();


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();
			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API V1");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
