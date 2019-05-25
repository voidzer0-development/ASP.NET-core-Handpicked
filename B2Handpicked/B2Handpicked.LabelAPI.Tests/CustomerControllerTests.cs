using System;
using System.Collections.Generic;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using B2Handpicked.LabelAPI.Authentication;
using B2Handpicked.LabelAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace B2Handpicked.LabelAPI.Tests {
    public class CustomerControllerTests {
        private readonly Customer _testCustomer1 = new Customer {
            Id = 1,
            Email = "testperson1@gmail.com",
            Name = "TestPerson1",
            PhoneNumber = "0612345678",
        };

        private readonly Customer _testCustomer2 = new Customer {
            Id = 2,
            Email = "testperson2@gmail.com",
            Name = "TestPerson2",
            PhoneNumber = "0612345678",
        };
        private readonly Customer _testCustomer3 = new Customer {
            Id = 3,
            Email = "testperson3@gmail.com",
            Name = "TestPerson3",
            PhoneNumber = "0612345678",
        };

        private readonly IAuthentication _fakeAuth;

        public CustomerControllerTests() {
            var authMock = new Mock<IAuthentication>();
            authMock.Setup(repo => repo.HasAccess(It.IsAny<HttpContext>(), It.IsAny<Invoice>())).Returns(true);
            authMock.Setup(repo => repo.SetAuthentication(It.IsAny<HttpContext>(), It.IsAny<Invoice>()));
            _fakeAuth = authMock.Object;
        }

        [Fact]
        public void Get_CallsFilterOnce() {
            //Arrange
            var repoMock = new Mock<IRepository<Customer>>();
            repoMock.Setup(repo => repo.Filter(It.IsAny<Func<Customer, bool>>())).Returns(new List<Customer> { });
            var controller = new CustomersController(repoMock.Object, _fakeAuth);

            //Act
            controller.Get();

            //Assert
            repoMock.Verify(repo => repo.Filter(It.IsAny<Func<Customer, bool>>()), Times.Once);
        }

        [Fact]
        public async void GetById_CallsRepoGetByIdOnce() {
            //Arrange
            var repoMock = new Mock<IRepository<Customer>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(_testCustomer1);
            var controller = new CustomersController(repoMock.Object, _fakeAuth);

            //Act
            await controller.Get(_testCustomer1.Id);

            //Assert
            repoMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetCustomer_ReturnsNotFoundWhenCustomerIsNull() {
            //Arrange
            var repoMock = new Mock<IRepository<Customer>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(null as Customer);
            var controller = new CustomersController(repoMock.Object, _fakeAuth);

            //Act
            var result = await controller.Get(_testCustomer1.Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}