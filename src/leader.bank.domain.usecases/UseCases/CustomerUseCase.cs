using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using leader.bank.domain.usecases.Gateways;
using leader.bank.domain.usecases.Gateways.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.UseCases
{
    public class CustomerUseCase : ICustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetCustomersAsync();
        }

        public async Task<List<CustomerWithAccountAndCard>> GetCustomerWithAccountAndCard(int id)
        {
            return await _customerRepository.GetCustomerWithAccountAndCard(id);
        }

        public async Task<InsertNewCustomer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id)
        {
            return await _customerRepository.GetCustomerWithAccountsAsync(id);
        }

        public async Task<List<CustomerWithAccounts>> GetDoneTransactionById(int id)
        {
            return await _customerRepository.GetDoneTransactionAsync(id);
        }




    }
}
