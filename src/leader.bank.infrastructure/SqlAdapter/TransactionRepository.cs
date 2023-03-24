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
    public class TransactionRepository : ITransactionRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string tableName = "Transactions";
        private readonly string tableName2 = "Accounts";
        private readonly string tableName3 = "Cards";
        private readonly string tableName4 = "Customers";



        public TransactionRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetTransactionAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName}";
            var result = await connection.QueryAsync<Transaction>(sqlQuery);
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


        public async Task<InsertNewTransaction> CreateTransactionAsync(Transaction transaction)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var transactionAAgregar = new Transaction
            {
                Id_Account = transaction.Id_Account,
                TransactionDate = DateTime.Now.ToString("MM/dd/yyyy"),
                TransactionHour = DateTime.Now.ToString("H:mm"),
                TransactionType = transaction.TransactionType,
                Description = transaction.Description,
                Amount = transaction.Amount,
                OldBalance = transaction.OldBalance,
                FinalBalance = transaction.FinalBalance,
                TransactionState = transaction.TransactionState
            };
            Transaction.Validate(transactionAAgregar);

            string sqlQuery = $"INSERT INTO {tableName} (Id_Account, TransactionDate,TransactionHour,TransactionType,Description,Amount,OldBalance,FinalBalance,TransactionState)VALUES(@idAccount, @transactionDate, @transactionHour,@transacitonType,@description,@amount,@oldBalance,@finalBalance,@transactionState)";
            var rows = await connection.ExecuteAsync(sqlQuery, transactionAAgregar);
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
