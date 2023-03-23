using leader.bank.domain.Commands;
using leader.bank.domain.Entities;

namespace leader.bank.domain.usecases.Gateways
{
    public interface ICardUseCase
    {
        Task<List<Card>> GetCardsAsync();
        Task<InsertNewCard> CreateCardAsync(Card card);
    }
}
