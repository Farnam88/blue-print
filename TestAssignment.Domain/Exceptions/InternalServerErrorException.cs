using System.Collections.Generic;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;

namespace TestAssignment.Domain.Exceptions
{
    public class InternalServerErrorException : ExceptionBase
    {
        public InternalServerErrorException(
            string message = ErrorMessages.InternalServerError, IList<ErrorDetail> info = null) : base(message,
            ErrorCodes.InternalServerError,
            info)
        {
        }
    }
}