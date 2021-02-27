using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Model.Dto;

namespace WebApi.Mapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<PaymentDto, Payment>().ReverseMap();
		}
	}
}
