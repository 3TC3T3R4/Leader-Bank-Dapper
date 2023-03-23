using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.domain.usecases.Gateways
{
    public interface IAccountUseCase
    {
        Task<List<Account>> GetAccountsAsync();
        Task<InsertNewAccount> CreateAccountAsync(Account account);
    }
}
