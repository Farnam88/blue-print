using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Common.Enums;

namespace BluePrint.Domain.Exceptions;

public class InternalServerErrorException : ExceptionBase
{
    public InternalServerErrorException(
        string message = ErrorMessages.InternalServerError, IList<ErrorDetail> info = null!) : base(message,
        ErrorCodes.InternalServerError,
        info)
    {
    }
}