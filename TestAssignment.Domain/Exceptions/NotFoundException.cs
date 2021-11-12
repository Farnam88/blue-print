using System.Collections.Generic;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;

namespace TestAssignment.Domain.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message = ErrorMessages.NotFound,
            IList<ErrorDetail> info = null) : base(
            message, ErrorCodes.NotFound, info)
        {
        }
    }
}