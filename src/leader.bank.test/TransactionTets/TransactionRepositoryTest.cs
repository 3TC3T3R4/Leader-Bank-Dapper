using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using Moq;


namespace leader.bank.test.TransactionTets
{
   public class TransactionRepositoryTest
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;

        public TransactionRepositoryTest()
        {
            _mockTransactionRepository = new();
        }

        [Fact]
        public async Task GetTransactionAsync(){
            //Arrange
            var transaction = new Transaction{
                Id_Account = 3,
                TransactionDate = "02/23/2023",
                TransactionHour = "14:21",
                TransactionType = "Retiro",
                Description = "Retiro para mercar",
                Amount = 0,
                OldBalance = 0,
                FinalBalance =0,
                TransactionState = "Aprobada"
            };
            var transactionList = new List<Transaction> { transaction };
            _mockTransactionRepository.Setup(x => x.GetTransactionAsync()).ReturnsAsync(transactionList);

            //Act
            var result = await _mockTransactionRepository.Object.GetTransactionAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(transactionList, result);

        }

        [Fact]
        public async Task CreateTransactionAsync()
        {
            //arrange
            var newTransaction = new Transaction
            {
                Id_Account = 3,
                TransactionDate = "02/23/2023",
                TransactionHour = "14:21",
                TransactionType = "Retiro",
                Description = "Retiro para mercar",
                Amount = 0,
                OldBalance = 0,
                FinalBalance = 0,
                TransactionState = "Aprobada"
            };
            var transactionCreated = new InsertNewTransaction
            {
                Id_Account = 3,
                TransactionType = "Retiro",
                Description = "Retiro para mercar",
                Amount = 0,
                OldBalance = 0,
                FinalBalance = 0,
                TransactionState = "Aprobada"

            };
            _mockTransactionRepository.Setup(x => x.CreateTransactionAsync(newTransaction)).ReturnsAsync(transactionCreated);
            //act
            var result = await _mockTransactionRepository.Object.CreateTransactionAsync(newTransaction);
            //assert
            Assert.Equal(transactionCreated, result);
        }









    }
}
