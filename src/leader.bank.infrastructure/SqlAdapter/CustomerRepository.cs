using AutoMapper;
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
        private readonly string tableName = "Customers";
        private readonly IMapper _mapper;

        public CustomerRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
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

        public async Task<List<CustomerWithAccountAndCard>> GetCustomerWithAccountAndCard(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName} cus " +
                                $"INNER JOIN Accounts a ON a.Id_Customer = @id " +
                                $"INNER JOIN Cards c ON c.Id_Account = a.Account_Id " +
                                $"WHERE cus.Customer_Id = @id";
            var customer = await connection.QueryAsync<CustomerWithAccountAndCard, AccountWithCardOnly,
                Card, CustomerWithAccountAndCard>(sqlQuery, (c, a, card) =>
            {
                c.Account = a;
                c.Account.Card = card;
                return c;
            },
            new { id },
            splitOn: "Account_Id, Card_Id");

            if (customer.IsNullOrEmpty())
            {
                throw new Exception("The customer doesn't exist or doesn't have an account or card assigned.");
            }
            connection.Close();
            return customer.ToList();
        }


        public async Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var customerQuery = $"SELECT * FROM {tableName} WHERE Customer_Id = @id";
            var accountQuery = $"SELECT * FROM Accounts WHERE Id_Customer = @id";
            var multiQuery = $"{customerQuery};{accountQuery}";

            using (var multi = await connection.QueryMultipleAsync(multiQuery, new { id }))
            {
                var customer = await multi.ReadFirstOrDefaultAsync<Customer>();
                var accounts = await multi.ReadAsync<Account>();

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
        }
    }
}