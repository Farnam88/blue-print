using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Core.DataLayer;
using TestAssignment.Core.Infrastructure.DAL;
using TestAssignment.CoreTests.Helpers;
using TestAssignment.CoreTests.Helpers.TestSpecifications;
using TestAssignment.Models;
using Xunit;

namespace TestAssignment.CoreTests
{
    public class AsyncRepositoryTests
    {
        private readonly Mock<IDbContext> _mockDbContext = new();

        [Fact]
        public async Task FindAsync_ShouldReturnEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestAssignmentEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestAssignmentEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.FindAsync(new object[] {1}, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            _mockDbContext.Setup(s => s.Set<TestAssignmentEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestAssignmentEntity> sut =
                new AsyncRepository<TestAssignmentEntity>(_mockDbContext.Object);

            var actualResult = await sut.FindAsync(1, default);

            //Assert

            _mockDbContext.Verify(v => v.Set<TestAssignmentEntity>());

            mockDbSet.Verify(v => v.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()));

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Title, actualResult.Title);
            Assert.Equal(expectedResult.CreateDateTime, actualResult.CreateDateTime);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_OnSuccess()
        {
            //Arrange

            var entity = new TestAssignmentEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestAssignmentEntity>();

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet
                .Setup(s => s.AddAsync(entity, It.IsAny<CancellationToken>()))
                .Verifiable();

            _mockDbContext.Setup(s => s.Set<TestAssignmentEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestAssignmentEntity> sut =
                new AsyncRepository<TestAssignmentEntity>(_mockDbContext.Object);

            await sut.AddAsync(entity, It.IsAny<CancellationToken>());

            //Assert
            _mockDbContext.Verify(v => v.Set<TestAssignmentEntity>());

            mockDbSet.Verify(v => v.AddAsync(It.IsAny<TestAssignmentEntity>(),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddEntities_OnSuccess()
        {
            //Arrange

            var entities = new List<TestAssignmentEntity>
            {
                new()
                {
                    Id = 1,
                    Title = "Test",
                    CreateDateTime = DateTime.UtcNow
                },
                new()
                {
                    Id = 2,
                    Title = "Test2",
                    CreateDateTime = DateTime.UtcNow.AddDays(1)
                }
            };

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet
                .Setup(s => s.AddRangeAsync(entities, It.IsAny<CancellationToken>()))
                .Verifiable();

            _mockDbContext.Setup(s => s.Set<TestAssignmentEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestAssignmentEntity> sut =
                new AsyncRepository<TestAssignmentEntity>(_mockDbContext.Object);

            await sut.AddRangeAsync(entities, It.IsAny<CancellationToken>());

            //Assert
            _mockDbContext.Verify(v => v.Set<TestAssignmentEntity>());

            mockDbSet.Verify(v => v.AddRangeAsync(It.IsAny<IEnumerable<TestAssignmentEntity>>(),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestAssignmentEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestAssignmentEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.FindAsync(new object[] {1}, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            mockDbSet.Setup(s => s.Remove(expectedResult))
                .Verifiable();

            _mockDbContext.Setup(s => s.Set<TestAssignmentEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestAssignmentEntity> sut =
                new AsyncRepository<TestAssignmentEntity>(_mockDbContext.Object);

            await sut.DeleteAsync(1, It.IsAny<CancellationToken>());

            //Assert

            _mockDbContext.Verify(v => v.Set<TestAssignmentEntity>());

            mockDbSet.Verify(v => v.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()));
            mockDbSet.Verify(v => v.Remove(It.IsAny<TestAssignmentEntity>()));
        }

        [Fact]
        public void Delete_ShouldDeleteEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestAssignmentEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestAssignmentEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.Remove(expectedResult))
                .Verifiable();

            _mockDbContext.Setup(s => s.Set<TestAssignmentEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestAssignmentEntity> sut =
                new AsyncRepository<TestAssignmentEntity>(_mockDbContext.Object);

            sut.Delete(expectedResult);

            //Assert

            _mockDbContext.Verify(v => v.Set<TestAssignmentEntity>());

            mockDbSet.Verify(v => v.Remove(It.IsAny<TestAssignmentEntity>()));
        }
    }
}