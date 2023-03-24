using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using Microsoft.IdentityModel.Tokens;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string _tableNameTransac = "Transactions";
        private readonly string _tableNameAccount = "Accounts";

        public TransactionRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetTransactionAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {_tableNameTransac}";
            var transactions = await connection.QueryAsync<Transaction>(sqlQuery);

            if (transactions.IsNullOrEmpty())
            {
                throw new Exception("There aren't transactions to show.");
            }

            connection.Close();
            return transactions.ToList();
        }


        public async Task<InsertNewTransaction> CreateTransactionAsync(Transaction transaction)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            //verify that the account exists and it balance
            var sqlAccount = $"SELECT * FROM {_tableNameAccount} WHERE Account_Id = {transaction.Id_Account}";
            var accountToUpdate = await connection.QuerySingleAsync<Account>(sqlAccount) ?? throw new Exception("The account doesn't exist");

            var transactionAAgregar = new Transaction
            {
                Id_Account = transaction.Id_Account,
                TransactionDate = DateTime.Now.ToString("MM/dd/yyyy"),
                TransactionHour = DateTime.Now.ToString("H:mm"),
                TransactionType = transaction.TransactionType,
                Description = transaction.Description,
                Amount = transaction.Amount,
                OldBalance = accountToUpdate.Balance,
                TransactionState = transaction.TransactionState
            };
            switch (transactionAAgregar.TransactionType)
            {
                case "Deposito":
                    transactionAAgregar.FinalBalance = transactionAAgregar.OldBalance + transactionAAgregar.Amount;
                    break;
                case "Retiro":
                    if (transactionAAgregar.OldBalance >= transactionAAgregar.Amount)
                    {
                        transactionAAgregar.FinalBalance = transactionAAgregar.OldBalance - transactionAAgregar.Amount;
                    }
                    else
                    {
                        throw new Exception("There isn't enough money.");
                    }
                    break;
                case "Pago":
                    if (transactionAAgregar.OldBalance >= transactionAAgregar.Amount)
                    {
                        transactionAAgregar.FinalBalance = transactionAAgregar.OldBalance - transactionAAgregar.Amount;
                    }
                    else
                    {
                        throw new Exception("There isn't enough money.");
                    }
                    break;
            }

            Transaction.Validate(transactionAAgregar);

            //update the account balance
            var sqlUpdate = $"UPDATE {_tableNameAccount} SET Balance = {transactionAAgregar.FinalBalance} " +
                $"WHERE Account_Id = {transactionAAgregar.Id_Account}";

            var accountUpdated = await connection.ExecuteAsync(sqlUpdate);

            if (accountUpdated != 1)
            {
                throw new Exception("The account was not updated.");
            }

            string sqlQuery = $"INSERT INTO {_tableNameTransac} (Id_Account, TransactionDate, TransactionHour, TransactionType, " +
                $"Description, Amount,OldBalance, FinalBalance, TransactionState) " +
                $"VALUES (@Id_Account, @TransactionDate, @TransactionHour, @TransactionType, @Description, @Amount, @OldBalance, " +
                $"@FinalBalance, @TransactionState)";
            var transactionCreated = await connection.ExecuteAsync(sqlQuery, transactionAAgregar);

            if (transactionCreated != 1)
            {
                throw new Exception("The transaction was not created.");
            }

            return _mapper.Map<InsertNewTransaction>(transaction);
        }

        //public async Task<List<CustomerWithAccounts>> GetDoneTransactionAsync(int id){
        //    var connection = await _dbConnectionBuilder.CreateConnectionAsync();
        //    var sqlQuery = $"SELECT  *  FROM {tableName4} cus " +
        //        $"INNER JOIN  {tableName2} ac " +
        //        $"ON  cus.Customer_Id  = ac.Id_Customer " +
        //         $" INNER JOIN {tableName3} car " +
        //         $" ON ac.Account_Id = car.Id_Account " +
        //         $"INNER JOIN {tableName} tra " +
        //         $"ON  tra.Id_Account = ac.Account_Id " +
        //        $"WHERE  cus.Customer_Id = @id";
        //    var customer = await connection.QueryAsync<CustomerWithAccounts,AccountWithCardAndTransactions,Card,Transaction, CustomerWithAccounts>(sqlQuery, (cwc,act ,card,t) =>
        //       {
        //           cwc.Accounts = new List<AccountWithCardAndTransactions>();
        //           cwc.Accounts.Add(act);
        //           act.Card = card;
        //           act.Transactions = new List<Transaction>();
        //           act.Transactions.Add(t);



        //           return cwc;

        //       },
        //   new { id },
        //   splitOn: "Account_Id , Card_Id , Transaction_Id");
        //    connection.Close();
        //    return  customer.ToList();

        //}
    }
}
