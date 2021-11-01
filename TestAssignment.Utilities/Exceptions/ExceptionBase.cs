using System;
using System.Collections.Generic;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Common.Enums;

namespace TestAssignment.Utilities.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        private readonly IList<ErrorDetail> _additionalInfo;
        private readonly ErrorCodes _errorCode;

        protected ExceptionBase(string message, ErrorCodes errorCode = ErrorCodes.InternalServerError,
            IList<ErrorDetail> info = null) : base(message)
        {
            _errorCode = errorCode;
            _additionalInfo = info ?? new List<ErrorDetail>();
        }

        public IList<ErrorDetail> AdditionalInfo => _additionalInfo;
        public ErrorCodes ErrorCode => _errorCode;
    }
}