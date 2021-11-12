using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;
using TestAssignment.Domain.Exceptions;
using Xunit;

namespace TestAssignment.DomainTests
{
    public class ResultModelTests
    {
        [Theory]
        [InlineData(400, ErrorCodes.InvalidRequest)]
        [InlineData(404, ErrorCodes.NotFound)]
        [InlineData(500, ErrorCodes.InternalServerError)]
        [InlineData(505, ErrorCodes.ObjectNull)]
        [InlineData(507, ErrorCodes.InternalServerError)]
        [InlineData(200, ErrorCodes.Success)]
        public void ResultModel_ShouldReturnsError_WithRelativeErrorCode(int errorCode, ErrorCodes expectedResult)
        {
            //Arrange and Act
            ResultModel<object> result = errorCode switch
            {
                200 => ResultModel<object>.Success(),
                400 => ResultModel<object>.Fail(new InvalidRequestException()),
                404 => ResultModel<object>.Fail(new NotFoundException()),
                500 => ResultModel<object>.Fail(new InternalServerErrorException()),
                505 => ResultModel<object>.Fail(new ObjectNullException()),
                _ => ResultModel<object>.Fail(new InternalServerErrorException())
            };

            //Assert
            Assert.Equal(expectedResult, result.ErrorCode);
        }
    }
}