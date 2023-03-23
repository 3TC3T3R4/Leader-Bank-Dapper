using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.api.AutoMapper
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<InsertNewCustomer, Customer>().ReverseMap();
            CreateMap<InsertNewAccount, Account>().ReverseMap();            
            CreateMap<InsertNewCard, Card>().ReverseMap();
            CreateMap<InsertNewTransaction, Transaction>().ReverseMap();
        }
    }
}
