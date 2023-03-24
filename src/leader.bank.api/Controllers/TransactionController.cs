using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using leader.bank.domain.usecases.Gateways;
using Microsoft.AspNetCore.Mvc;


namespace leader.bank.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionUseCase _transactionUseCase;
        private readonly IMapper _mapper;


        public TransactionController(ITransactionUseCase transactionUseCase, IMapper mapper)
        {
            _transactionUseCase = transactionUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _transactionUseCase.GetTransaction();
        }

        [HttpPost]
        public async Task<InsertNewTransaction> CreateTransactionAsync([FromBody] InsertNewTransaction command)
        {
            return await _transactionUseCase.AddTransaction(_mapper.Map<Transaction>(command));
        }


        [HttpGet("{id:int}")]

        public async Task<List<CustomerWithAccounts>>Get_DoneTransaction_By_AccountAndCardAsync(int id)
        {
            return await _transactionUseCase.GetDoneTransactionById(id);
        }



    }
}
