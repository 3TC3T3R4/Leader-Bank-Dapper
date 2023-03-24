using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using leader.bank.domain.usecases.Gateways.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace leader.bank.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardUseCase _cardUseCase;
        private readonly IMapper _mapper;

        public CardController(ICardUseCase cardUseCase, IMapper mapper)
        {
            _cardUseCase = cardUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Card>> GetCardsAsync()
        {
            return await _cardUseCase.GetCardsAsync();
        }

        [HttpPost]
        public async Task<InsertNewCard> CreateCardAsync([FromBody] InsertNewCard newCard)
        {
            return await _cardUseCase.CreateCardAsync(_mapper.Map<Card>(newCard));
        }
    }
}
