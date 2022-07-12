using BluePrint.Application.Data.Contexts;
using BluePrint.Application.Data.Repositories;
using BluePrint.Domain.Entities;
using BluePrint.Infrastructure.Data.Repositories;
using Moq;
using BluePrint.Infrastructure.UnitTests.Helpers;

namespace BluePrint.Infrastructure.UnitTests
{
    public class AsyncRepositoryTests
    {
        private readonly Mock<IDbContext> _mockDbContext = new();

        [Fact]
        public async Task FindAsync_ShouldReturnEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestTodoEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestTodoEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.FindAsync(new object[] {1}, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            _mockDbContext.Setup(s => s.EntitySet<TestTodoEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestTodoEntity> sut =
                new AsyncRepository<TestTodoEntity>(_mockDbContext.Object);

            var actualResult = await sut.FindAsync(1, default);

            //Assert

            _mockDbContext.Verify(v => v.EntitySet<TestTodoEntity>());

            mockDbSet.Verify(v => v.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()));

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Title, actualResult.Title);
            Assert.Equal(expectedResult.CreateDateTime, actualResult.CreateDateTime);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_OnSuccess()
        {
            //Arrange

            var entity = new TestTodoEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestTodoEntity>();

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet
                .Setup(s => s.AddAsync(entity, It.IsAny<CancellationToken>()))
                .Verifiable();

            _mockDbContext.Setup(s => s.EntitySet<TestTodoEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestTodoEntity> sut =
                new AsyncRepository<TestTodoEntity>(_mockDbContext.Object);

            await sut.AddAsync(entity, It.IsAny<CancellationToken>());

            //Assert
            _mockDbContext.Verify(v => v.EntitySet<TestTodoEntity>());

            mockDbSet.Verify(v => v.AddAsync(It.IsAny<TestTodoEntity>(),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddEntities_OnSuccess()
        {
            //Arrange

            var entities = new List<TestTodoEntity>
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

            _mockDbContext.Setup(s => s.EntitySet<TestTodoEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestTodoEntity> sut =
                new AsyncRepository<TestTodoEntity>(_mockDbContext.Object);

            await sut.AddRangeAsync(entities, It.IsAny<CancellationToken>());

            //Assert
            _mockDbContext.Verify(v => v.EntitySet<TestTodoEntity>());

            mockDbSet.Verify(v => v.AddRangeAsync(It.IsAny<IEnumerable<TestTodoEntity>>(),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestTodoEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestTodoEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.FindAsync(new object[] {1}, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            mockDbSet.Setup(s => s.Remove(expectedResult))
                .Verifiable();

            _mockDbContext.Setup(s => s.EntitySet<TestTodoEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestTodoEntity> sut =
                new AsyncRepository<TestTodoEntity>(_mockDbContext.Object);

            await sut.DeleteAsync(1, It.IsAny<CancellationToken>());

            //Assert

            _mockDbContext.Verify(v => v.EntitySet<TestTodoEntity>());

            mockDbSet.Verify(v => v.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()));
            mockDbSet.Verify(v => v.Remove(It.IsAny<TestTodoEntity>()));
        }

        [Fact]
        public void Delete_ShouldDeleteEntity_OnSuccess()
        {
            //Arrange
            var expectedResult = new TestTodoEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            var entities = new List<TestTodoEntity> {expectedResult};

            var mockDbSet = entities.GetQueryableMockDbSet();

            mockDbSet.Setup(s => s.Remove(expectedResult))
                .Verifiable();

            _mockDbContext.Setup(s => s.EntitySet<TestTodoEntity>())
                .Returns(mockDbSet.Object)
                .Verifiable();

            //Act
            IAsyncRepository<TestTodoEntity> sut =
                new AsyncRepository<TestTodoEntity>(_mockDbContext.Object);

            sut.Delete(expectedResult);

            //Assert

            _mockDbContext.Verify(v => v.EntitySet<TestTodoEntity>());

            mockDbSet.Verify(v => v.Remove(It.IsAny<TestTodoEntity>()));
        }
    }
}