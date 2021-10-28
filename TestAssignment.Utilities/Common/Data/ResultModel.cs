using System.Collections.Generic;
using TestAssignment.Utilities.Common.Enums;

#nullable enable
namespace TestAssignment.Utilities.Common.Data
{
    public class ResultModel<TOutput>
    {
        /// <summary>
        /// Success Result Constructor
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        /// <param name="message">Success Message</param>
        /// <param name="result">Result Object(Optional)</param>
        private ResultModel(ErrorCodes errorCode, string message, TOutput result = default!)
        {
            ErrorCode = errorCode;
            Message = message;
            IsSucceeded = true;
            Result = result;
            Error = null;
        }

        /// <summary>
        /// Fail Result Constructor
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        /// <param name="message">Error Message</param>
        /// <param name="info">Error Info(Optional)</param>
        private ResultModel(ErrorCodes errorCode, string message,
            IDictionary<string, string> info = null!)
        {
            ErrorCode = errorCode;
            Message = message;
            IsSucceeded = false;
            Result = default(TOutput);
            Error = new Error(info);
        }

        public ErrorCodes ErrorCode { get; }
        public string Message { get; }
        public bool IsSucceeded { get; }
        public TOutput? Result { get; }
        public Error? Error { get; }

        /// <summary>
        /// Creates Success Result
        /// </summary>
        /// <param name="message">Success Message</param>
        /// <param name="result">Result Object(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> Success(string message = "",
            TOutput result = default!)
        {
            return new ResultModel<TOutput>(ErrorCodes.Success, message, result);
        }

        /// <summary>
        /// Creates Failed Result
        /// </summary>
        /// <param name="errorCode">Error Code</param>
        /// <param name="message">Error Message</param>
        /// <param name="info">Error Info(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> Fail(ErrorCodes errorCode = ErrorCodes.InternalServerError,
            string message = "",
            IDictionary<string, string> info = null!)
        {
            return new ResultModel<TOutput>(errorCode, message, info);
        }

        /// <summary>
        /// Not Found Failed Result
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="info">Error Info(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> NotFound(string message = ErrorMessages.NotFound,
            IDictionary<string, string> info = null!)
        {
            return new ResultModel<TOutput>(ErrorCodes.NotFound, message, info);
        }

        /// <summary>
        /// Not Found Failed Result
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="info">Error Info(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> InvalidRequest(string message = ErrorMessages.InvalidRequest,
            IDictionary<string, string> info = null!)
        {
            return new ResultModel<TOutput>(ErrorCodes.InvalidRequest, message, info);
        }

        /// <summary>
        /// Not Found Failed Result
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="info">Error Info(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> ServerError(string message = ErrorMessages.InternalServerError,
            IDictionary<string, string> info = null!)
        {
            return new ResultModel<TOutput>(ErrorCodes.InternalServerError, message, info);
        }

        /// <summary>
        /// Not Found Failed Result
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="info">Error Info(Optional)</param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> ObjectNull(string message = ErrorMessages.ObjectNull,
            IDictionary<string, string> info = null!)
        {
            return new ResultModel<TOutput>(ErrorCodes.ObjectNull, message, info);
        }
    }
}