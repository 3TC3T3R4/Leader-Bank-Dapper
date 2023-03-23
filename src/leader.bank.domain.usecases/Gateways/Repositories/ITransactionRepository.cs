using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface ITransactionRepository
    {


        Task<InsertNewTransaction> CreateTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetTransactionAsync();



    }
}
