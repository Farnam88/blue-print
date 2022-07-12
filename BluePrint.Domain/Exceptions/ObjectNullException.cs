using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Common.Enums;

namespace BluePrint.Domain.Exceptions;

public class ObjectNullException : ExceptionBase
{
    public ObjectNullException(string message = ErrorMessages.ObjectNull,
        IList<ErrorDetail> info = null!) : base(message, ErrorCodes.ObjectNull, info)
    {
    }
}