﻿using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();

        Task<CustomerWithAccountAndCard> GetCustomerWithAccountAndCard(int id);

        Task<InsertNewCustomer> CreateCustomerAsync(Customer customer);

        Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id);


        Task<CustomerWithAccounts> GetDoneTransactionAsync(int id);

    }
}
