using AutoMapper;
using KSO_SalesHub.Models.Simulation.Dto;
using KSO_SalesHub.Models.Simulation.Entities;

namespace KSO_SalesHub.Utilities
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
			CreateMap<M_Station, StationDto>().ReverseMap();
			CreateMap<T_SummaryCalculator, CalculatorDto>().ReverseMap();
			CreateMap<T_SummaryCalculator, SummaryCalculatorDto>().ReverseMap();
			CreateMap<T_SummaryCalculatorDetail, SummaryCalculatorDetailDto>().ReverseMap(); 
		}
    }
}
