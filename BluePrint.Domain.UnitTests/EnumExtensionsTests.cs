using BluePrint.Domain.UnitTests.Helpers;
using BluePrint.Domain.Extensions;
using Xunit;

namespace BluePrint.Domain.UnitTests
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