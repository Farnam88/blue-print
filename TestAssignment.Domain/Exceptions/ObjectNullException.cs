using System.Collections.Generic;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;

namespace TestAssignment.Domain.Exceptions
{
    public class ObjectNullException : ExceptionBase
    {
        public ObjectNullException(string message = ErrorMessages.ObjectNull,
            IList<ErrorDetail> info = null) : base(message, ErrorCodes.ObjectNull, info)
        {
        }
    }
}