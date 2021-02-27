using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using SharedComponent.Services;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPaymentService _paymentService;
		public PaymentController(IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_paymentService = paymentService;
		}

		[HttpPost]
		public async Task<IActionResult> ProcessPayment(PaymentDto model)
		{
			try
			{
				var dbModel = _mapper.Map<Payment>(model);
				dbModel.PaymentStateId = (int)SharedComponent.Enum.Enum.PaymentState.Pending;
				var isProcess = await CallGatewayService(model.Amount);
				if (isProcess)
				{
					dbModel.PaymentStateId = (int)SharedComponent.Enum.Enum.PaymentState.Processed;
				}
				else
				{
					dbModel.PaymentStateId = (int)SharedComponent.Enum.Enum.PaymentState.Failure;
				}
				_unitOfWork.Payments.Add(dbModel);
				_unitOfWork.Complete();
				return Ok();

			}
			catch (Exception e)
			{
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		private async Task<bool> CallGatewayService(decimal amount)
		{
			return await _paymentService.ProcessPaymentAsync(amount);
		} 
	}
}