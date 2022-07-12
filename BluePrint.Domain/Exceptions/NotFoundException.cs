using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Common.Enums;

namespace BluePrint.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message = ErrorMessages.NotFound,
        IList<ErrorDetail> info = null!) : base(
        message, ErrorCodes.NotFound, info)
    {
    }
}