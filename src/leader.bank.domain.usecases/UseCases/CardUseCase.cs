using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using leader.bank.domain.usecases.Gateways.Repositories;

namespace leader.bank.domain.usecases.UseCases
{
    public class CardUseCase : ICardUseCase
    {
        private readonly ICardRepository _cardRepository;
        public CardUseCase(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<InsertNewCard> CreateCardAsync(Card card)
        {
            return await _cardRepository.CreateCardAsync(card);
        }

        public async Task<List<Card>> GetCardsAsync()
        {
            return await _cardRepository.GetCardsAsync();
        }
    }
}
