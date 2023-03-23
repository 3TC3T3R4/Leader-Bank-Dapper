using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string tableName = "customers";
        private readonly IMapper _mapper;

        public CustomerRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper )
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName}";
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

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName} WHERE Customer_id = @Customer_id";
            var result = await connection.QuerySingleAsync<Customer>(sqlQuery, new { id });
            connection.Close();
            return result;
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

            string sqlQuery = $"INSERT INTO {tableName} (Names, Surnames, Address, Email, Phone, Birthdate, Occupation, Gender) VALUES (@Names, @Surnames, @Address, @Email, @Phone, @Birthdate, @Occupation, @Gender )";
            var result = await connection.ExecuteAsync(sqlQuery, createCustomer);
            connection.Close();
            return _mapper.Map<InsertNewCustomer>(createCustomer);

        }

    }
}