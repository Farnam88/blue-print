using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Core.DataLayer;
using TestAssignment.Core.Infrastructure.DAL;
using TestAssignment.CoreTests.Helpers;
using TestAssignment.Models;
using Xunit;

namespace TestAssignment.CoreTests
{
    public class AsyncRepositoryTests
    {
        private readonly Mock<IDbContext> _mockDbContext = new();

        [Fact]
        public async Task FindAsync_ShouldReturnEntity_IfExists()
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
    }
}