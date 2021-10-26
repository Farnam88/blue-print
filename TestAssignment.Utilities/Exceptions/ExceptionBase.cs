using System;
using System.Collections.Generic;
using TestAssignment.Utilities.Common.Enums;

namespace TestAssignment.Utilities.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        private readonly IDictionary<string, string> _additionalInfo;
        private readonly ErrorCodes _errorCode;

        protected ExceptionBase(string message, ErrorCodes errorCode = ErrorCodes.InternalServerError,
            IDictionary<string, string> info = null) : base(message)
        {
            _errorCode = errorCode;
            _additionalInfo = info ?? new Dictionary<string, string>();
        }

        public IDictionary<string, string> AdditionalInfo => _additionalInfo;
        public ErrorCodes ErrorCode => _errorCode;
    }
}