using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.Entities.Wrappers;
using leader.bank.domain.usecases.Gateways.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.test.CustomerTests
{
    public class CustomerRepositoryTest
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        public CustomerRepositoryTest()
        {
            _mockCustomerRepository = new();
        }

        [Fact]
        public async Task GetCustomersAsync()
        {
            //Arrange
            var customer = new Customer
            {
                Names = "Juan",
                Surnames = "Perez",
                Address = "Calle 1",
                Email = "juan@mail.com",
                Phone = "123456789",
                Birthdate = DateTime.Now,
                Occupation = "celador",
                Gender = "M"
            };
            var customerList = new List<Customer> { customer };
            _mockCustomerRepository.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customerList);

            //Act
            var result = await _mockCustomerRepository.Object.GetCustomersAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(customerList, result);

        }

        [Fact]
        public async Task CreateCustomerAsync()
        {
            //arrange
            var newCustomer = new Customer
            {
                Names = "Santiago Baquero",
                Surnames = "Perez",
                Address = "Calle 1",
                Email = "",
                Phone = "123456789",
                Birthdate = DateTime.Now,
                Occupation = "cajero",
                Gender = "M"
            };
            var customerCreated = new InsertNewCustomer
            {
                Names = "Santiago Baquero",
                Surnames = "Perez",
                Address = "Calle 1",
                Email = "",
                Phone = "123456789",
                Birthdate = DateTime.Now,
                Occupation = "cajero",
                Gender = "M"

            };
            _mockCustomerRepository.Setup(x => x.CreateCustomerAsync(newCustomer)).ReturnsAsync(customerCreated);
            //act
            var result = await _mockCustomerRepository.Object.CreateCustomerAsync(newCustomer);
            //assert
            Assert.Equal(customerCreated, result);
        }


        [Fact]

        public async Task GetCustomerWithAccountAndCard()
        {
            //arrange
            var customer = new Customer
            {
                Names = "Juan",
                Surnames = "Perez",
                Address = "Calle 1",
                Email = "",
                Phone = "123456789",
                Birthdate = DateTime.Now,
                Occupation = "asesor",
                Gender = "M"
            };
            var account = new Account
            {
                Account_Id = 1,
                Id_Customer = 1,
                AccountType = "Ahorros",
                Balance = 1000,
                OpenDate = DateTime.Now,
                CloseDate = null,
                ManagementCost = 0,
                AccountState = "Activo"

            };
            var card = new Card
            {
                Card_Id = 1,
                Id_Account = 1,
                NumberCard = "123123",
                Cvc = "123",
                EmissionDate = DateTime.Now,
                ExpirationDate = DateTime.Now,
                CardState = "Activo"
            };

            var customerWithAccountAndCard = new CustomerWithAccountAndCard
            {
                Customer_Id = 1,
                Names = "Juan",
                Surnames = "Perez",
                Address = "Calle 1",
                Email = "",
                Phone = "123456789",
                Birthdate = DateTime.Now,
                Occupation = "asesor",
                Gender = "M"


            };
            var customerWithAccountAndCardList = new List<CustomerWithAccountAndCard> { customerWithAccountAndCard };
            _mockCustomerRepository.Setup(x => x.GetCustomerWithAccountAndCard(1)).ReturnsAsync(customerWithAccountAndCardList);
            //act
            var result = await _mockCustomerRepository.Object.GetCustomerWithAccountAndCard(1);
            //assert
            Assert.NotNull(result);
            Assert.Equal(customerWithAccountAndCardList, result);
        }

        [Fact]


        



    }


}
