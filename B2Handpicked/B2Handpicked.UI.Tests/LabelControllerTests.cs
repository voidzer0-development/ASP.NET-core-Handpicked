using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using B2Handpicked.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace B2Handpicked.UI.Tests {
    public class LabelControllerTests {
        private readonly Label _testLabel1 = new Label {
            Id = 1,
            Name = "Test label 1",
            Token = "XY123Z"
        };

        private readonly Label _testLabel2 = new Label {
            Id = 2,
            Name = "Test label 2",
            Token = "ABC987"
        };

        [Fact]
        public async void Index_ReturnsCorrectView() {
            // Arrange
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetAllAsList()).ReturnsAsync(new List<Label> { });
            var controller = new LabelsController(repoMock.Object);

            // Act
            var result = (ViewResult) await controller.Index();

            // Assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public async void Index_CallsGetAllAsListOnce() {
            // Arrange
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetAllAsList()).ReturnsAsync(new List<Label> { });
            var controller = new LabelsController(repoMock.Object);

            // Act
            await controller.Index();

            // Assert
            repoMock.Verify(repo => repo.GetAllAsList(), Times.Once);
        }

        [Fact]
        public async void Index_ReturnsCorrectList() {
            // Arrange
            var list = new List<Label> { _testLabel1, _testLabel2 };
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetAllAsList()).ReturnsAsync(list);
            var controller = new LabelsController(repoMock.Object);

            // Act
            var result = (ViewResult) await controller.Index();

            // Assert
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async void Details_ReturnsCorrectView() {
            // Arrange
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(_testLabel1);
            var controller = new LabelsController(repoMock.Object);

            // Act
            var result = (ViewResult) await controller.Details(_testLabel1.Id);

            // Assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public async void Details_CallsGetByIdOnce() {
            // Arrange
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(_testLabel1);
            var controller = new LabelsController(repoMock.Object);

            // Act
            await controller.Details(_testLabel1.Id);

            // Assert
            repoMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void Details_ReturnsNotFound_WhenLabelIsNull() {
            // Arrange
            var repoMock = new Mock<IRepository<Label>>();
            repoMock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(null as Label);
            var controller = new LabelsController(repoMock.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
