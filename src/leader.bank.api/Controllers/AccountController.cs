using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace leader.bank.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountUseCase _accountUseCase;
        private readonly IMapper _mapper;
        public AccountController(IAccountUseCase accountUseCase, IMapper mapper)
        {
            _accountUseCase = accountUseCase;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _accountUseCase.GetAccountsAsync();
        }
        [HttpPost]
        public async Task<InsertNewAccount> CreateAccountAsync([FromBody] InsertNewAccount newAccount)
        {
            return await _accountUseCase.CreateAccountAsync(_mapper.Map<Account>(newAccount));
        }
    }
}
