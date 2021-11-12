using System.Collections.Generic;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;

namespace TestAssignment.Domain.Exceptions
{
    public class InvalidRequestException : ExceptionBase
    {
        public InvalidRequestException(
            string message = ErrorMessages.InvalidRequest,
            IList<ErrorDetail> info = null) : base(message, ErrorCodes.InvalidRequest, info)
        {
        }
    }
}