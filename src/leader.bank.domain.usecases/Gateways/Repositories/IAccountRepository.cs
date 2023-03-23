using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsAsync();
        Task<InsertNewAccount> CreateAccountAsync(Account account);
    }
}
