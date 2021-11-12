using System;
using System.Collections.Generic;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;

namespace TestAssignment.Domain.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        private readonly IList<ErrorDetail> _additionalInfo;
        private readonly ErrorCodes _errorCode;

        protected ExceptionBase(string message, ErrorCodes errorCode,
            IList<ErrorDetail> info = null) : base(message)
        {
            _errorCode = errorCode;
            _additionalInfo = info ?? new List<ErrorDetail>();
        }

        protected ExceptionBase() : this(ErrorMessages.InternalServerError, ErrorCodes.InternalServerError, null)
        {
        }

        public IList<ErrorDetail> AdditionalInfo => _additionalInfo;

        public ErrorCodes ErrorCode()
        {
            return _errorCode;
        }
    }
}