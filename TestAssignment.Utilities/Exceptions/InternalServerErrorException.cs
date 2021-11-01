using System.Collections.Generic;
using TestAssignment.Utilities.Common.Data;

namespace TestAssignment.Utilities.Exceptions
{
    public class InternalServerErrorException : ExceptionBase
    {
        public InternalServerErrorException(
            string message = ErrorMessages.InternalServerError, IList<ErrorDetail> info = null) : base(message,
            info: info)
        {
        }
    }
}