using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Moq;
using SharedComponent.Mapper;
using SharedComponent.Services;
using System;
using System.Net;
using WebApi.Controllers;
using Xunit;

namespace PaymentApi.Tests.Controllers
{
	public class PaymentControllerTests
	{
		private readonly Mock<IPaymentService> _paymentServiceMock;
		private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private PaymentController _controllerToTest;

		/// <summary>
		/// constructor
		/// </summary>
		public PaymentControllerTests()
		{
			_paymentServiceMock = new Mock<IPaymentService>();
			_paymentRepositoryMock = new Mock<IPaymentRepository>();
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			Dependencies();
		}


		/// <summary>
		/// Mocked dependencies
		/// </summary>
		private void Dependencies()
		{
			//auto mapper configuration
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperProfile());
			});
			var mapper = mockMapper.CreateMapper();
			_paymentServiceMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<decimal>())).ReturnsAsync(true);
			_paymentRepositoryMock.Setup(x => x.Add(It.IsAny<Payment>()));
			_unitOfWorkMock.Setup(uow => uow.Payments).Returns(_paymentRepositoryMock.Object);
			_controllerToTest = new PaymentController(_unitOfWorkMock.Object, mapper, _paymentServiceMock.Object);
		}

		/// <summary>
		/// Test success case
		/// </summary>
		[Fact]
		public void ProcessPayment_SuccessAsync()
		{
			var model = new PaymentDto()
			{
				Amount = 23,
				CardHolder = "usman",
				ExpirationDate = DateTime.Now.AddDays(30),
				CreditCardNumber = "11-11-11",
				SecurityCode = "123"

			};
			// Act
			var actualResult = _controllerToTest.ProcessPayment(model).Result as OkResult;

			// Assert
			Assert.Equal((int)HttpStatusCode.OK, actualResult?.StatusCode);
		}
	}
}
