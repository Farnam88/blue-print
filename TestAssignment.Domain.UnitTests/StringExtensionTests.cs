using TestAssignment.Domain.Extensions;
using Xunit;

namespace TestAssignment.Domain.UnitTests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void HasString_ShouldReturnFalse_WhenStringDoesNotHaveValue(string str)
        {
            //Act

            var result = StringExtensions.HasString(str);

            //Assert

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ContainsString_ShouldReturnFalse_WhenStringDoesNotHaveValue(string str)
        {
            //Act

            var result = str.ContainsString();

            //Assert

            Assert.False(result);
        }

        [Theory]
        [InlineData("Farnam")]
        [InlineData(" Farnam ")]
        public void HasString_ShouldReturnTrue_WhenStringDoesNotHaveValue(string str)
        {
            //Act

            var result = StringExtensions.HasString(str);

            //Assert

            Assert.True(result);
        }

        [Theory]
        [InlineData("Farnam", "FARNAM")]
        [InlineData(" Farnam ", "FARNAM")]
        [InlineData(" Farnam jamshIDian ", "FARNAM JAMSHIDIAN")]
        public void ToNormalize_ShouldTrimAndUpperCaseString_OnSuccess(string str, string expectedResult)
        {
            //Act

            var result = str.ToNormalize();

            //Assert

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Farnam")]
        [InlineData(" Farnam ")]
        public void ContainsString_ShouldReturnTrue_WhenStringDoesNotHaveValue(string str)
        {
            //Act

            var result = str.ContainsString();

            //Assert

            Assert.True(result);
        }
    }
}