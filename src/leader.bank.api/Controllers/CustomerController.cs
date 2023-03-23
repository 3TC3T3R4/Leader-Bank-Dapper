using AutoMapper;
using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace leader.bank.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {

        private readonly ICustomerUseCase _customerUseCase;
        private readonly IMapper _mapper;


        public DirectorController(ICustomerUseCase customerUseCase, IMapper mapper)
        {
            _customerUseCase = customerUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerUseCase.GetCustomersAsync();
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerUseCase.GetCustomerByIdAsync(id);
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<InsertNewCustomer> CreateCustomerAsync(InsertNewCustomer newCustomer)
        {
            return await _customerUseCase.CreateCustomerAsync(_mapper.Map<Customer>(newCustomer));
        }
    }
}
