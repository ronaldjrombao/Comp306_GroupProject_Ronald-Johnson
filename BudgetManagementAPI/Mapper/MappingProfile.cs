using AutoMapper;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;

namespace BudgetManagementAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Budget, BudgetItem>()
                .ForMember(dest => dest.BudgetType, opt => opt.MapFrom(src => src.BudgetType.CategoryName));
            CreateMap<PutBudgetDto, Budget>()
                .ForMember(dest => dest.BudgetType, opt => opt.Ignore());
            CreateMap<PatchBudgetDto, Budget>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}
