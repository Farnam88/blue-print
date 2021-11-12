using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Application.Contexts.TestAssignmentServices.Queries;
using TestAssignment.Domain.Common.Data;
using TestAssignment.WebApi.Controllers.TestAssignmentEndpoints;
using Xunit;

namespace TestAssignment.ApiTests
{
    public class TestAssignmentControllerTests
    {
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetAll_ShouldReturnAllTestAssignments_OnSuccess()
        {
            //Arrange

            var resultDtos = CreateDtoList();

            var expectedResult = ResultModel<IList<TestAssignmentDto>>.Success("", resultDtos);

            _mediatorMock.Setup(s => s.Send(It.IsAny<GetTestAssignmentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            GetAllTestAssignments sut = new GetAllTestAssignments(_mediatorMock.Object);

            //Act

            var actualResult = await sut.HandleAsync(It.IsAny<CancellationToken>());

            //Assert

            _mediatorMock.Verify(v => v.Send(It.IsAny<GetTestAssignmentsQuery>(), It.IsAny<CancellationToken>()));

            Assert.True(actualResult.Value.Result != null && actualResult.Value.IsSucceeded &&
                        actualResult.Value.Result.Count > 0);
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