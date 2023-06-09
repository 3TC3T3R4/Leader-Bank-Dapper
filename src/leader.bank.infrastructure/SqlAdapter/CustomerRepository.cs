﻿using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using Microsoft.IdentityModel.Tokens;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;

        private readonly string _tableNameTransac = "Transactions";
        private readonly string _tableNameAccounts = "Accounts";
        private readonly string _tableNameCards = "Cards";
        private readonly string _tableNameCustomers = "Customers";

        private readonly IMapper _mapper;

        public CustomerRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCustomers}";
            var result = await connection.QueryAsync<Customer>(sqlQuery);
            if
            (
                result.IsNullOrEmpty()
            )
            {
                throw new Exception("No records found");
            }
            connection.Close();
            return result.ToList();
        }

        public async Task<InsertNewCustomer> CreateCustomerAsync(Customer customer)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var createCustomer = new Customer
            {
                Names = customer.Names,
                Surnames = customer.Surnames,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                Birthdate = customer.Birthdate,
                Occupation = customer.Occupation,
                Gender = customer.Gender

            };
            Customer.Validate(createCustomer);

            string sqlQuery = $"INSERT INTO {_tableNameCustomers} (Names, Surnames, Address, Email, Phone, Birthdate, Occupation, Gender) VALUES (@Names, @Surnames, @Address, @Email, @Phone, @Birthdate, @Occupation, @Gender )";
            var result = await connection.ExecuteAsync(sqlQuery, createCustomer);
            connection.Close();
            return _mapper.Map<InsertNewCustomer>(createCustomer);

        }

        public async Task<CustomerWithAccountAndCard> GetCustomerWithAccountAndCard(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameCustomers} cus " +
                                $"INNER JOIN Accounts a ON a.Id_Customer = @id " +
                                $"INNER JOIN Cards c ON c.Id_Account = a.Account_Id " +
                                $"WHERE cus.Customer_Id = @id";

            var customerAll = new CustomerWithAccountAndCard();
            var customer = await connection.QueryAsync<CustomerWithAccountAndCard, AccountWithCardOnly,
                Card, CustomerWithAccountAndCard>(sqlQuery, (c, ac, card) =>
                {
                    if (customerAll.Customer_Id.Equals(0))
                    {
                        customerAll = c;
                    }

                    customerAll.Accounts.Add(ac);
                    ac.Card = card;

                    return c;
                },
            new { id },
            splitOn: "Account_Id, Card_Id");

            if (customer.IsNullOrEmpty())
            {
                throw new Exception("The customer doesn't exist or doesn't have an account or card assigned.");
            }
            connection.Close();
            return customerAll;
        }


        public async Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var customerQuery = $"SELECT * FROM {_tableNameCustomers} WHERE Customer_Id = @id";
            var accountQuery = $"SELECT * FROM Accounts WHERE Id_Customer = @id";
            var multiQuery = $"{customerQuery};{accountQuery}";

            using var multi = await connection.QueryMultipleAsync(multiQuery, new { id });
            var customer = await multi.ReadFirstOrDefaultAsync<Customer>();
            var accounts = await multi.ReadAsync<Account>();

            connection.Close();
            return new CustomerWithAccountsOnly
            {
                Customer_id = customer.Customer_Id,
                Names = customer.Names,
                Surnames = customer.Surnames,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                Birthdate = customer.Birthdate,
                Occupation = customer.Occupation,
                Gender = customer.Gender,
                Accounts = accounts.ToList()
            };
        }

        public async Task<CustomerWithAccounts> GetDoneTransactionAsync(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var customerQuery = $"SELECT * FROM {_tableNameCustomers} WHERE Customer_Id = @id";
            var accountsQuery = $"SELECT * FROM {_tableNameAccounts} WHERE Id_Customer = @id";
            var multiQuery = $"{customerQuery};{accountsQuery}";

            using var multi = await connection.QueryMultipleAsync(multiQuery, new { id });

            var customer = await multi.ReadFirstOrDefaultAsync<Customer>();
            var accounts = await multi.ReadAsync<AccountWithCardAndTransactions>();

            var customerAll = new CustomerWithAccounts
            {
                Customer_id = customer.Customer_Id,
                Names = customer.Names,
                Surnames = customer.Surnames,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                Birthdate = customer.Birthdate,
                Occupation = customer.Occupation,
                Gender = customer.Gender,
                Accounts = accounts.ToList()
            };
            foreach (var account in customerAll.Accounts)
            {
                var cardQuery = $"SELECT * FROM {_tableNameCards} WHERE Id_Account = {account.Account_id}";
                var card = await connection.QuerySingleAsync<Card>(cardQuery);
                account.Card = card;
                var transactionsQuery = $"SELECT * FROM {_tableNameTransac} WHERE Id_Account = {account.Account_id}";
                var transactions = await connection.QueryAsync<Transaction>(transactionsQuery);
                account.Transactions = transactions.ToList();
            }

            connection.Close();
            return customerAll;

            //var sqlQuery = $"SELECT  *  FROM {tableName4} cus " +
            //                $"INNER JOIN  {tableName2} ac " +
            //                $"ON  cus.Customer_Id  = ac.Id_Customer " +
            //                $" INNER JOIN {tableName3} car " +
            //                $" ON ac.Account_Id = car.Id_Account " +
            //                $"INNER JOIN {tableName} tra " +
            //                $"ON  tra.Id_Account = ac.Account_Id " +
            //                $"WHERE  cus.Customer_Id = @id ";

            //var customer = await connection.QueryAsync<CustomerWithAccounts, AccountWithCardAndTransactions, Card, Transaction, CustomerWithAccounts>(sqlQuery, (cwc, act, card, t) =>
            //{
            //    cwc.Accounts.Add(act);
            //    act.Card = card;
            //    act.Transactions.Add(t);

            //    return cwc;
            //},
            //new { id },
            //splitOn: "Account_Id , Card_Id , Transaction_Id");
        }
    }
}