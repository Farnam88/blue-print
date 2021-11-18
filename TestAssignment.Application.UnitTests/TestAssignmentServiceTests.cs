using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Moq;
using TestAssignment.Application.Contexts.TestAssignmentServices.Commands;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Application.Contexts.TestAssignmentServices.Queries;
using TestAssignment.Application.Contexts.TestAssignmentServices.Specifications;
using TestAssignment.Application.Data;
using TestAssignment.Application.Data.Repositories;
using TestAssignment.Domain.Common.Enums;
using TestAssignment.Domain.Entities;
using Xunit;

namespace TestAssignment.Application.UnitTests
{
    public class TestAssignmentServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();

        [Fact]
        public async Task GetTestAssignmentQueryHandler_ShouldReturnALIstOfTestAssignmentDto_OnSuccess()
        {
            //Arrange

            var expectedTestAssignmentResult = CreateDtoList();

            Mock<IAsyncRepository<TestAssignmentEntity>> repositoryMock =
                new Mock<IAsyncRepository<TestAssignmentEntity>>();


            repositoryMock.Setup(s =>
                    s.ToListAsync(It.IsAny<TestAssignmentSpecificationResult>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTestAssignmentResult)
                .Verifiable();

            _uowMock.Setup(s => s.TestAssignmentRepository)
                .Returns(repositoryMock.Object)
                .Verifiable();

            GetTestAssignmentQueryHandler sut = new GetTestAssignmentQueryHandler(_uowMock.Object);

            //Act
            var actualResult = await sut.Handle(new GetTestAssignmentsQuery(), It.IsAny<CancellationToken>());

            //Assert

            repositoryMock.Verify(v => v.ToListAsync(It.IsAny<Specification<TestAssignmentEntity, TestAssignmentDto>>(),
                It.IsAny<CancellationToken>()));

            _uowMock.Verify(v => v.TestAssignmentRepository);

            Assert.True(actualResult.Result != null && actualResult.IsSucceeded && actualResult.Result.Count > 0);
        }

        [Fact]
        public async Task CreateTestAssignmentCommandHandler_ShouldCreateTestAssignment_OnSuccess()
        {
            //Arrange

            Mock<IAsyncRepository<TestAssignmentEntity>> repositoryMock =
                new Mock<IAsyncRepository<TestAssignmentEntity>>();

            TestAssignmentEntity entity = new TestAssignmentEntity
            {
                Id = 1,
                Title = "Test",
                CreateDateTime = DateTime.UtcNow
            };

            repositoryMock.Setup(s =>
                    s.AddAsync(entity, It.IsAny<CancellationToken>()))
                .Verifiable();

            _uowMock.Setup(s => s.TestAssignmentRepository)
                .Returns(repositoryMock.Object)
                .Verifiable();

            _uowMock.Setup(s => s.CommitAsync(It.IsAny<CancellationToken>()))
                .Verifiable();

            CreateTestAssignmentCommandHandler sut = new CreateTestAssignmentCommandHandler(_uowMock.Object);

            //Act
            var actualResult = await sut.Handle(new CreateTestAssignmentCommand
            {
                Title = "Test"
            }, It.IsAny<CancellationToken>());

            //Assert

            repositoryMock.Verify(v => v.AddAsync(It.IsAny<TestAssignmentEntity>(),
                It.IsAny<CancellationToken>()));

            _uowMock.Verify(v => v.TestAssignmentRepository);

            _uowMock.Verify(v => v.CommitAsync(It.IsAny<CancellationToken>()));

            Assert.True(actualResult.IsSucceeded && actualResult.ErrorCode == ErrorCodes.Success);
        }

        private IList<TestAssignmentDto> CreateDtoList()
        {
            return new List<TestAssignmentDto>
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