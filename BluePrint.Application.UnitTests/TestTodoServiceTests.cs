using Ardalis.Specification;
using BluePrint.Application.Contexts.TestTodoServices.Commands;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Application.Contexts.TestTodoServices.Queries;
using BluePrint.Application.Contexts.TestTodoServices.Specifications;
using Moq;
using BluePrint.Application.Data;
using BluePrint.Application.Data.Repositories;
using BluePrint.Domain.Common.Enums;
using BluePrint.Domain.Entities;

namespace BluePrint.Application.UnitTests
{
    public class TestTodoServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();

        [Fact]
        public async Task GetTestDtoQueryHandler_ShouldReturnALIstOfTestAssignmentDto_OnSuccess()
        {
            //Arrange

            var expectedTestAssignmentResult = CreateDtoList();

            Mock<IAsyncRepository<TestTodoEntity>> repositoryMock =
                new Mock<IAsyncRepository<TestTodoEntity>>();


            repositoryMock.Setup(s =>
                    s.ToListAsync(It.IsAny<TestTodoSpecificationResult>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTestAssignmentResult)
                .Verifiable();

            _uowMock.Setup(s => s.TestTodoRepository)
                .Returns(repositoryMock.Object)
                .Verifiable();

            GetTestAssignmentQueryHandler sut = new GetTestAssignmentQueryHandler(_uowMock.Object);

            //Act
            var actualResult = await sut.Handle(new GetTestAssignmentsQuery(), It.IsAny<CancellationToken>());

            //Assert

            repositoryMock.Verify(v => v.ToListAsync(It.IsAny<Specification<TestTodoEntity, TestTodoDto>>(),
                It.IsAny<CancellationToken>()));

            _uowMock.Verify(v => v.TestTodoRepository);

            Assert.True(actualResult.Result != null && actualResult.IsSucceeded && actualResult.Result.Count > 0);
        }

        [Fact]
        public async Task CreateTestTodoCommandHandler_ShouldCreateTestAssignment_OnSuccess()
        {
            //Arrange

            Mock<IAsyncRepository<TestTodoEntity>> repositoryMock =
                new Mock<IAsyncRepository<TestTodoEntity>>();

            TestTodoEntity entity = new TestTodoEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            repositoryMock.Setup(s =>
                    s.AddAsync(entity, It.IsAny<CancellationToken>()))
                .Verifiable();

            _uowMock.Setup(s => s.TestTodoRepository)
                .Returns(repositoryMock.Object)
                .Verifiable();

            _uowMock.Setup(s => s.CommitAsync(It.IsAny<CancellationToken>()))
                .Verifiable();

            CreateTestTodoCommandHandler sut = new CreateTestTodoCommandHandler(_uowMock.Object);

            //Act
            var actualResult = await sut.Handle(new CreateTestTodoCommand
            {
                Title = "Test"
            }, It.IsAny<CancellationToken>());

            //Assert

            repositoryMock.Verify(v => v.AddAsync(It.IsAny<TestTodoEntity>(),
                It.IsAny<CancellationToken>()));

            _uowMock.Verify(v => v.TestTodoRepository);

            _uowMock.Verify(v => v.CommitAsync(It.IsAny<CancellationToken>()));

            Assert.True(actualResult.IsSucceeded && actualResult.ErrorCode == ErrorCodes.Success);
        }

        private IList<TestTodoDto> CreateDtoList()
        {
            return new List<TestTodoDto>
            {
                new()
                {
                    Id = 1,
                    Title = "Test"
                }
            };
        }
    }
}