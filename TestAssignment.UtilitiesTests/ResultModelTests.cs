using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Common.Enums;
using Xunit;

namespace TestAssignment.UtilitiesTests
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
                400 => ResultModel<object>.InvalidRequest(),
                404 => ResultModel<object>.NotFound(),
                500 => ResultModel<object>.ServerError(),
                505 => ResultModel<object>.ObjectNull(),
                _ => ResultModel<object>.Fail()
            };

            //Assert
            Assert.Equal(expectedResult, result.ErrorCode);
        }
    }
}