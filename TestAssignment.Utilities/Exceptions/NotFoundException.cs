using System.Collections.Generic;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Common.Enums;

namespace TestAssignment.Utilities.Exceptions
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