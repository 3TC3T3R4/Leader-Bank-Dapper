using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using leader.bank.domain.usecases.Gateways.Repositories;

namespace leader.bank.domain.usecases.UseCases
{
    public class AccountUseCase : IAccountUseCase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<InsertNewAccount> CreateAccountAsync(Account account)
        {
            return await _accountRepository.CreateAccountAsync(account);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _accountRepository.GetAccountsAsync();
        }
    }
}
