using B2Handpicked.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace B2Handpicked.Infrastructure.Tests {
    public class EFRepositoryTests {
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

        private static readonly Random _random = new Random();

        private ApplicationDbContext GetDbContext() {
            var _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: $"UnitTestDb{_random.Next(1000)}").Options;
            return new ApplicationDbContext(_dbOptions);
        }

        [Fact]
        public async void Create_ReturnsTrueAndAddsModelToDatabase() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);

                // Act
                bool result = await repo.Create(_testLabel1);

                // Assert
                Assert.True(result);
                Assert.Single(dbContext.Labels);
                Assert.Equal(dbContext.Labels.Find(_testLabel1.Id), _testLabel1);
            }
        }

        [Fact]
        public async void DoesExist_ReturnsTrue_WhenEntityIsInDbAndIdIsValid() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);
                await repo.Create(_testLabel1);

                // Act
                bool result = await repo.DoesExist(_testLabel1.Id);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async void DoesExist_ReturnsFalse_WhenIdIsNull() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);

                // Act
                bool result = await repo.DoesExist(null);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async void DoesExist_ReturnsFalse_WhenEntityIsNotInDbAndIdIsValid() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);

                // Act
                bool result = await repo.DoesExist(999);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async void Delete_ShouldDelete() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);
                await repo.Create(_testLabel1);

                // Act
                bool result = await repo.Delete(_testLabel1);

                // Assert
                Assert.True(result);
                Assert.Empty(dbContext.Labels);
            }
        }

        [Fact]
        public async void DeleteById_ShouldDelete() {
            using (var dbContext = GetDbContext()) {
                // Arange
                var repo = new EFLabelRepository(dbContext);
                await repo.Create(_testLabel1);

                // Act
                bool result = await repo.Delete(_testLabel1.Id);

                // Assert
                Assert.True(result);
                Assert.Empty(dbContext.Labels);
            }
        }
    }
}
