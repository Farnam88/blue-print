using TestAssignment.Utilities.Extensions;
using TestAssignment.UtilitiesTests.Helpers;
using Xunit;

namespace TestAssignment.UtilitiesTests
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