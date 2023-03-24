using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using Moq;

namespace leader.bank.test.AccountTests
{
    public class AccountRepositoryTest
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;

        public AccountRepositoryTest()
        {
            _mockAccountRepository = new();
        }

        [Fact]
        public async Task CreateAccountAsync()
        {
            //Arrange
            var accountToCreate = new Account
            {
                Id_Customer = 1,
                AccountType = "Savings",
                Balance = 1000,
                OpenDate = DateTime.Now,
                CloseDate = null,
                ManagementCost = 0,
                AccountState = "Active",
            };

            var accountCreated = new InsertNewAccount
            {
                Id_Customer = 1,
                AccountType = "Savings",
                Balance = 1000,
                ManagementCost = 0,
                AccountState = "Active",
            };
            _mockAccountRepository.Setup(x => x.CreateAccountAsync(accountToCreate)).ReturnsAsync(accountCreated);

            //Act
            var result = await _mockAccountRepository.Object.CreateAccountAsync(accountToCreate);

            //Assert
            Assert.Equal(accountCreated, result);
        }

        [Fact]
        public async Task GetAccountsAsync()
        {
            //Arrange
            var account = new Account
            {
                Id_Customer = 1,
                AccountType = "Savings",
                Balance = 1000,
                OpenDate = DateTime.Now,
                CloseDate = null,
                ManagementCost = 0,
                AccountState = "Active",
            };
            var accountList = new List<Account> { account };

            _mockAccountRepository.Setup(x => x.GetAccountsAsync()).ReturnsAsync(accountList);

            //Act
            var result = await _mockAccountRepository.Object.GetAccountsAsync();

            //Assert
            Assert.Equal(accountList, result);
        }
    }
}
