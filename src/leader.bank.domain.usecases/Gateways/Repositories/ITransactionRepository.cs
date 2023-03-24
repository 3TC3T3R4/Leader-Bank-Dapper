using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface ITransactionRepository
    {


        Task<InsertNewTransaction> CreateTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetTransactionAsync();

        //Task<List<CustomerWithAccounts>> GetDoneTransactionAsync(int id);





    }
}
