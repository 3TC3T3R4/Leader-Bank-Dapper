using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using Microsoft.IdentityModel.Tokens;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string _tableName = "Accounts";

        public AccountRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<InsertNewAccount> CreateAccountAsync(Account account)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            //verify that customer exist
            var customerSql = $"SELECT COUNT(*) FROM Customers WHERE Customer_id = @idCustomer;";
            var customerCount = await connection.ExecuteScalarAsync<int>(customerSql, new { idCustomer = account.Id_Customer });

            if (customerCount == 0)
            {
                throw new Exception("The customer doesn't exist.");
            }

            var accountToCreate = new Account
            {
                Id_Customer = account.Id_Customer,
                AccountType = account.AccountType,
                Balance = account.Balance,
                OpenDate = account.OpenDate,
                CloseDate = null,
                ManagementCost = account.ManagementCost,
                AccountState = account.AccountState,
            };


            Account.Validate(accountToCreate);

            var sql = $"INSERT INTO {_tableName} (Id_Customer, AccountType, Balance, OpenDate, CloseDate, ManagementCost, AccountState) " +
                $"VALUES (@Id_Customer, @AccountType, @Balance, @OpenDate, @CloseDate, @ManagementCost, @AccountState);";

            var result = await connection.ExecuteScalarAsync(sql, accountToCreate);
            connection.Close();
            return _mapper.Map<InsertNewAccount>(accountToCreate);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync() ;

            var sql = $"SELECT * FROM {_tableName}";
            var accounts = await connection.QueryAsync<Account>(sql);

            if (accounts.IsNullOrEmpty())
            {
                throw new Exception("There aren't accounts to show.");
            }
            connection.Close();
            return accounts.ToList();
        }
    }
}
