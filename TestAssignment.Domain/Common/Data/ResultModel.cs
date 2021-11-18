using System;
using System.Text.Json.Serialization;
using TestAssignment.Domain.Common.Enums;
using TestAssignment.Domain.Exceptions;

#nullable enable
namespace TestAssignment.Domain.Common.Data
{
    [Serializable]
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
        /// <param name="exception">Error Code</param>
        /// <param name="message">Error Message</param>
        /// <param name="info">Error Info(Optional)</param>
        private ResultModel(ExceptionBase exception)
        {
            ErrorCode = exception.ErrorCode();
            Message = exception.Message;
            IsSucceeded = false;
            Result = default(TOutput);
            Error = new Error(exception.AdditionalInfo);
        }

        public ResultModel(ErrorCodes errorCode, string message, bool isSucceeded, TOutput? result, Error? error)
        {
            ErrorCode = errorCode;
            Message = message;
            IsSucceeded = isSucceeded;
            Result = result;
            Error = error;
        }

        [JsonPropertyName("errorCode")] public ErrorCodes ErrorCode { get; }
        [JsonPropertyName("message")] public string Message { get; }
        [JsonPropertyName("isSucceeded")] public bool IsSucceeded { get; }
        [JsonPropertyName("result")] public TOutput? Result { get; }
        [JsonPropertyName("error")] public Error? Error { get; }

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
        /// <param name="exception"></param>
        /// <returns>ResultModel</returns>
        public static ResultModel<TOutput> Fail(ExceptionBase exception)
        {
            return new ResultModel<TOutput>(exception);
        }
    }
}