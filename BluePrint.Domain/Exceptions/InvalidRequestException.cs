using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Common.Enums;

namespace BluePrint.Domain.Exceptions;

public class InvalidRequestException : ExceptionBase
{
    public InvalidRequestException(
        string message = ErrorMessages.InvalidRequest,
        IList<ErrorDetail> info = null!) : base(message, ErrorCodes.InvalidRequest, info)
    {
    }
}