using AutoMapper;
using Dapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using leader.bank.infrastructure.Gateway;

namespace leader.bank.infrastructure.SqlAdapter
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly IMapper _mapper;
        private readonly string tableName = "Transactions";

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
            connection.Close();
            return result.ToList();
        }


        public async Task<InsertNewTransaction> CreateTransactionAsync(Transaction transaction)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var transactionAAgregar = new
            {
                idAccount = transaction.Id_Account,
                transactionDate = DateTime.Now.ToString("MM/dd/yyyy"),
                transactionHour = DateTime.Now.ToString("H:mm"),
                transacitonType = transaction.TransactionType,
                description = transaction.Description,
                amount = transaction.Amount,
                oldBalance = transaction.OldBalance,
                finalBalance = transaction.FinalBalance,
                transactionState = transaction.TransactionState
            };

            string sqlQuery = $"INSERT INTO {tableName} (Id_Account, TransactionDate,TransactionHour,TransactionType,Description,Amount,OldBalance,FinalBalance,TransactionState)VALUES(@idAccount, @transactionDate, @transactionHour,@transacitonType,@description,@amount,@oldBalance,@finalBalance,@transactionState)";
            var rows = await connection.ExecuteAsync(sqlQuery, transactionAAgregar);
            return _mapper.Map<InsertNewTransaction>(transaction);
        }






    }
}
