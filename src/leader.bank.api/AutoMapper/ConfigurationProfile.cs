using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.api.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            //CreateMap<InsertNewBranch, Branch>().ReverseMap();
            //CreateMap<InsertNewSupportStaff, SupportStaff>().ReverseMap();
            CreateMap<InsertNewCustomer, Customer>().ReverseMap();
            CreateMap<InsertNewAccount, Account>().ReverseMap();
            //CreateMap<InsertNewProduct, Product>().ReverseMap();
            CreateMap<InsertNewCard, Card>().ReverseMap();
            CreateMap<InsertNewTransaction, Transaction>().ReverseMap();
        }
    }
}
