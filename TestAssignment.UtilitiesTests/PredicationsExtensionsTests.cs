using System;
using TestAssignment.Utilities.Extensions;
using Xunit;

namespace TestAssignment.UtilitiesTests
{
    public class PredicationsExtensionsTests
    {
        [Theory]
        [InlineData("input Object", true)]
        [InlineData("", true)]
        [InlineData("", false)]
        public void PreconditionCheckNull_ShouldThrowArgumentNullException_WhenObjectIsNull(string name,
            bool nullObject)
        {
            //Arrange
            Object obj = new object();

            //Act
            if (nullObject)
                Assert.Throws<ArgumentNullException>(() => Preconditions.CheckNull((object) null, name));
            if (!nullObject)
                Preconditions.CheckNull(obj);
        }
    }
}