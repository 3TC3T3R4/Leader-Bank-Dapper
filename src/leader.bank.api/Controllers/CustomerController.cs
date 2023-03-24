using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using leader.bank.domain.usecases.Gateways;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace leader.bank.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerUseCase _customerUseCase;
        private readonly IMapper _mapper;


        public CustomerController(ICustomerUseCase customerUseCase, IMapper mapper)
        {
            _customerUseCase = customerUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerUseCase.GetCustomersAsync();
        }
               

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<InsertNewCustomer> CreateCustomerAsync(InsertNewCustomer newCustomer)
        {
            return await _customerUseCase.CreateCustomerAsync(_mapper.Map<Customer>(newCustomer));
        }

        [HttpGet]
        [Route("GetCustomerWithAccountAndCard")]
        public async Task<CustomerWithAccountAndCard> GetCustomerWithAccountAndCard(int id)
        {
            return await _customerUseCase.GetCustomerWithAccountAndCard(id);
        }

      
        [HttpGet]
        [Route("GetCustomerWithAccounts")]
        public async Task<CustomerWithAccountsOnly> GetCustomerWithAccountsAsync(int id)
        {
            return await _customerUseCase.GetCustomerWithAccountsAsync(id);
        }

        [HttpGet("GetCustomerWithAccount,Card,Transactions")]
        //[Route("GetCustomerWithAccountsAndCardsAndTransactions")]
        public async Task<CustomerWithAccounts> Get_DoneTransaction_By_AccountAndCardAsync(int id)
        {
            return await _customerUseCase.GetDoneTransactionById(id);

        }
    }
}
