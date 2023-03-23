using Dapper;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string tableName = "Transactions";

        public TransactionRepository(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }



        public async Task<List<Transaction>> GetTransactionAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            string sqlQuery = $"SELECT * FROM {tableName}";
            var result = await connection.QueryAsync<Transaction>(sqlQuery);
            connection.Close();
            return result.ToList();
        }


        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var transactionAAgregar = new
            {
                id = transaction.Transaction_Id  ,
                idAccount = transaction.Id_Account,
                transactionDate = transaction.TransactionDate,
                transactionHour = transaction.TransactionHour,
                transacitonType = transaction.TransactionType,
                description = transaction.Description,
                amount = transaction.Amount,
                oldBalance = transaction.OldBalance,
                finalBalance = transaction.FinalBalance,
                transactionState = transaction.TransactionState
            };
            string sqlQuery = $"INSERT INTO {tableName} (Id_Account, TransactionDate,TransactionHour,TransactionType,Description,Amount,OldBalance,FinalBalance,TransactionState)VALUES(@id, @ idAccount, @transactionDate, @transactionHour,@transacitonType,@description,@amount,@oldBalance,@finalBalance,@transactionState)";
            var rows = await connection.ExecuteAsync(sqlQuery, transactionAAgregar);
            return transaction;
        }






    }
}
