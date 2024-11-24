using AutoMapper;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;
using BudgetManagementAPI.Dto.Transaction;

namespace BudgetManagementAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Budget, BudgetItem>()
                .ForMember(dest => dest.BudgetType, opt => opt.MapFrom(src => src.BudgetType.CategoryName));
            CreateMap<Transaction, TransactionItem>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.CategoryName));
            CreateMap<PutBudgetDto, Budget>()
                .ForMember(dest => dest.BudgetType, opt => opt.Ignore());
            CreateMap<PatchBudgetDto, Budget>()
                .ForMember(dest => dest.BudgetType, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
            CreateMap<PutTransactionDto, Transaction>()
                .ForMember(dest => dest.TransactionType, opt => opt.Ignore());
            CreateMap<PatchTransactionDto, Transaction>()
                .ForMember(dest => dest.TransactionType, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}
