using leader.bank.domain.Commands;
using leader.bank.domain.Entities;
using leader.bank.domain.usecases.Gateways.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leader.bank.test.NewFolder
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

        //[Fact]
        //public async Task<InsertNewCustomer> CreateCustomerAsync(Customer customer)
        //{
        //    //arrange
        //    var newCustomer = new InsertNewCustomer
        //    {
        //        Names = "Santiago Baquero",
        //        Surnames = "Perez",
        //        Address = "Calle 1",
        //        Email = "",
        //        Phone = "123456789",
        //        Birthdate = DateTime.Now,
        //        Occupation = "cajero",
        //        Gender = "M"               
        //    };
        //    var customerCreated = new InsertNewCustomer
        //    {
        //        Names = "Santiago Baquero",
        //        Surnames = "Perez",
        //        Address = "Calle 1",
        //        Email = "",
        //        Phone = "123456789",
        //        Birthdate = DateTime.Now,
        //        Occupation = "cajero",
        //        Gender = "M"

        //    };
        //    _mockCustomerRepository.Setup(x => x.CreateCustomerAsync(newCustomer)).ReturnsAsync(customerCreated);
        //    //act
        //    var result = await _mockCustomerRepository.Object.CreateCustomerAsync(customerToCreate);
        //    //assert
        //    Assert.Equal(customerCreated, result);
        //}



    }
}
