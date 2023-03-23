using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.Gateways
{
    public interface ICustomerUseCase
    {
        Task<List<Customer>> GetCustomersAsync();

        Task<List<CustomerWithAccountAndCard>> GetCustomerWithAccountAndCard(int id);

        Task<InsertNewCustomer> CreateCustomerAsync(Customer customer);

        Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id);

    }
}
