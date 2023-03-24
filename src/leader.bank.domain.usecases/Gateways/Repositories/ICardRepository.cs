using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.domain.usecases.Gateways.Repositories
{
    public interface ICardRepository
    {
        Task<List<Card>> GetCardsAsync();
        Task<InsertNewCard> CreateCardAsync(Card card);
    }
}
