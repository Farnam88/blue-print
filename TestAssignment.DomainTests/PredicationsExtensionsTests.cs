using System;
using TestAssignment.Domain.Exceptions;
using TestAssignment.Domain.Extensions;
using Xunit;

namespace TestAssignment.DomainTests
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
                Assert.Throws<ObjectNullException>(() => Preconditions.CheckNull((object) null, name));
            if (!nullObject)
                Preconditions.CheckNull(obj);
        }
    }
}