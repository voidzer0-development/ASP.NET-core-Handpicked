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
    public class InvoiceControllerTests {
        private readonly Invoice _testInvoice1 = new Invoice {
            Id = 1,
            Number = 1,
            Label = new Label {
                Id = 1,
                Token = null
            }
        };

        private readonly Invoice _testInvoice2 = new Invoice {
            LabelId = 2,
            Number = 2
        };

        private readonly Invoice _testInvoice3 = new Invoice {
            Number = 2,
            CustomerId = 1234
        };

        private readonly IAuthentication _fakeAuth;

        public InvoiceControllerTests() {
            var authMock = new Mock<IAuthentication>();
            authMock.Setup(repo => repo.HasAccess(It.IsAny<HttpContext>(), It.IsAny<Invoice>())).Returns(true);
            authMock.Setup(repo => repo.SetAuthentication(It.IsAny<HttpContext>(), It.IsAny<Invoice>()));
            _fakeAuth = authMock.Object;
        }

        [Fact]
        public void Get_CallsFilterOnce() {
            // Arrange
            var repoMock = new Mock<IRepository<Invoice>>();
            repoMock.Setup(repo => repo.Filter(It.IsAny<Func<Invoice, bool>>())).Returns(new List<Invoice> { });
            var controller = new InvoicesController(repoMock.Object, _fakeAuth);

            // Act
            controller.Get();

            // Assert
            repoMock.Verify(repo => repo.Filter(It.IsAny<Func<Invoice, bool>>()), Times.Once);
        }

        [Fact]
        public async void GetById_CallsRepoGetByIdOnce() {
            // Arrange
            var repoMock = new Mock<IRepository<Invoice>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(_testInvoice1);
            var controller = new InvoicesController(repoMock.Object, _fakeAuth);

            // Act
            await controller.Get(_testInvoice1.Id);

            // Assert
            repoMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetById_ReturnsNotFound_WhenInvoiceIsNull() {
            // Arrange
            var repoMock = new Mock<IRepository<Invoice>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(null as Invoice);
            var controller = new InvoicesController(repoMock.Object, _fakeAuth);

            // Act
            var result = await controller.Get(_testInvoice1.Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
