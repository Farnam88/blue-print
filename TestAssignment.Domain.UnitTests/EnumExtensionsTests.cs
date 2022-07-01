using TestAssignment.Domain.Extensions;
using TestAssignment.Domain.UnitTests.Helpers;
using Xunit;

namespace TestAssignment.Domain.UnitTests
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData("Dog")]
        [InlineData("dOg")]
        [InlineData("dog")]
        public void StringToEnum_ShouldReturnEnum_whenStringIsConvertibleToEnum(string enumStr)
        {
            //Act
            var enumResult = enumStr.StringToEnum<TestEnum>();

            //Assert
            Assert.Equal(TestEnum.Dog, enumResult);
        }

        [Theory]
        [InlineData("wolf")]
        [InlineData("WolF")]
        [InlineData("Wolfe")]
        public void StringToEnum_ShouldReturnDefaultEnumValue_whenStringIsNotConvertibleToEnum(string enumStr)
        {
            //Act
            var enumResult = enumStr.StringToEnum<TestEnum>();

            //Assert
            Assert.Equal(default, enumResult);
        }
    }
}