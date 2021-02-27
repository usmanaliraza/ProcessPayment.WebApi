using AutoMapper;
using Domain.Entities;
using Model.Dto;

namespace SharedComponent.Mapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<PaymentDto, Payment>().ReverseMap();
		}
	}
}
