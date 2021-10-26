using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Exceptions;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.WebApi.Helpers.Attributes
{
    public class ValidRequestFilerAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        #region Overrides of ActionFilterAttribute

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = ResultModel<object>.InvalidRequest();
                
                context.Result = new ObjectResult(result)
                {
                    StatusCode = result.ErrorCode.ToStatusCode()
                };
                context.ModelState.Clear();
                base.OnActionExecuting(context);
            }
        }

        #endregion
    }

    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _handlers;

        public ExceptionHandlerFilterAttribute()
        {
            _handlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(InternalServerErrorException), HandleInternalServerErrorException},
                {typeof(InvalidRequestException), HandleInvalidRequestException},
                {typeof(ValidationException), HandleInvalidRequestException},
                {typeof(NotFoundException), HandleNotFoundException},
                {typeof(ObjectNullException), HandleObjectNullException},
            };
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ModelState.IsValid)
            {
                HandleInvalidRequestException(exceptionContext);
                base.OnException(exceptionContext);
            }
            else
            {
                HandleException(exceptionContext);

                base.OnException(exceptionContext);
            }
        }

        private void HandleException(ExceptionContext exceptionContext)
        {
            Type type = exceptionContext.Exception.GetType();
            if (_handlers.ContainsKey(type))
            {
                _handlers[type].Invoke(exceptionContext);
                return;
            }

            HandleUnknownException(exceptionContext);
        }

        private void HandleUnknownException(ExceptionContext exceptionContext)
        {
            var result = ResultModel<object>.ServerError("An unhandled exception occurred",
                new Dictionary<string, string>
                {
                    {"Exception Message", exceptionContext.Exception.Message}
                });

            SetObjectResult(exceptionContext, result);
        }

        private void HandleObjectNullException(ExceptionContext exceptionContext)
        {
            var exception = (ObjectNullException) exceptionContext.Exception;
            var result = ResultModel<object>.ObjectNull(info:
                exception.AdditionalInfo);

            result.Error?.AdditionalInfo.Add("Exception Message", exception.Message);

            SetObjectResult(exceptionContext, result);
        }

        private void HandleNotFoundException(ExceptionContext exceptionContext)
        {
            var exception = (NotFoundException) exceptionContext.Exception;
            var result = ResultModel<object>.NotFound(info:
                exception.AdditionalInfo);

            result.Error?.AdditionalInfo.Add("Exception Message", exception.Message);

            SetObjectResult(exceptionContext, result);
        }

        private void HandleInvalidRequestException(ExceptionContext exceptionContext)
        {
            // var exception = (InvalidRequestException) exceptionContext.Exception;
            if (exceptionContext.Exception is InvalidRequestException invalidRequestException)
            {
                var result = ResultModel<object>.InvalidRequest(
                    info: invalidRequestException.AdditionalInfo);

                result.Error?.AdditionalInfo.Add("Exception Message", invalidRequestException.Message);

                SetObjectResult(exceptionContext, result);

                exceptionContext.ModelState.Clear();
            }

            if (exceptionContext.Exception is ValidationException validationException)
            {
                // var errors=validationException.

                var result = ResultModel<object>.InvalidRequest();
                result.Error?.AdditionalInfo.Add("Exception Message", validationException.Message);

                SetObjectResult(exceptionContext, result);

                exceptionContext.ModelState.Clear();
            }
        }

        private void HandleInternalServerErrorException(ExceptionContext exceptionContext)
        {
            var exception = (InternalServerErrorException) exceptionContext.Exception;
            var result = ResultModel<object>.ServerError("An internal server error occurred",
                exception.AdditionalInfo);

            result.Error?.AdditionalInfo.Add("Exception Message", exception.Message);
            SetObjectResult(exceptionContext, result);
        }


        private void SetObjectResult(ExceptionContext exceptionContext, ResultModel<object> result)
        {
            exceptionContext.Result = new ObjectResult(result)
            {
                StatusCode = result.ErrorCode.ToStatusCode()
            };
            exceptionContext.ExceptionHandled = true;
        }
    }
}