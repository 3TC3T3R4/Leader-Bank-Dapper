using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using Moq;

namespace leader.bank.test.CardTest
{
    public class CardRepositoryTest
    {
        private readonly Mock<ICardRepository> _mockCardRepository;

        public CardRepositoryTest()
        {
            _mockCardRepository = new();
        }

        [Fact]
        public async Task CreateCardAsync()
        {
            //Arrange
            var cardCreated = new InsertNewCard
            {
                Id_Account = 1,
                NumberCard = "123456789",
                Cvc = "123",
                CardState = "Active"
            };

            var cardToCreate = new Card
            {
                Id_Account = 1,
                NumberCard = "123456789",
                Cvc = "123",
                EmissionDate = DateTime.Now,
                ExpirationDate = null,
                CardState = "Active",
            };

            _mockCardRepository.Setup(x => x.CreateCardAsync(cardToCreate)).ReturnsAsync(cardCreated);

            //Act
            var result = await _mockCardRepository.Object.CreateCardAsync(cardToCreate);

            //Assert
            Assert.Equal(cardCreated, result);
        }

        [Fact]
        public async Task GetCardsAsync()
        {
            //Arrange
            var card = new Card
            {
                Id_Account = 1,
                NumberCard = "123456789",
                Cvc = "123",
                EmissionDate = DateTime.Now,
                ExpirationDate = null,
                CardState = "Active",
            };
            var cardList = new List<Card> { card };

            _mockCardRepository.Setup(x => x.GetCardsAsync()).ReturnsAsync(cardList);

            //Act
            var result = await _mockCardRepository.Object.GetCardsAsync();

            //Assert
            Assert.Equal(cardList, result);
        }
    }
}
