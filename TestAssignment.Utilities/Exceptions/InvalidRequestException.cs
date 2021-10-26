using System.Collections.Generic;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Common.Enums;

namespace TestAssignment.Utilities.Exceptions
{
    public class InvalidRequestException : ExceptionBase
    {
        public InvalidRequestException(
            string message = ErrorMessages.InvalidRequest,
            IDictionary<string, string> info = null) : base(message, ErrorCodes.InvalidRequest, info)
        {
        }
    }
}