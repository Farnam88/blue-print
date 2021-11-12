using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Exceptions;
using TestAssignment.Domain.Extensions;

namespace TestAssignment.WebApi.Helpers.Attributes
{
    public sealed class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
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
            }
            else
            {
                HandleException(exceptionContext);
            }

            exceptionContext.ExceptionHandled = true;
            // base.OnException(exceptionContext);
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
            var result = ResultModel<object>.Fail(new InternalServerErrorException(info: new List<ErrorDetail>
            {
                new ErrorDetail("Exception Message", exceptionContext.Exception.Message)
            }));

            SetObjectResult(exceptionContext, result);
        }

        private void HandleObjectNullException(ExceptionContext exceptionContext)
        {
            var exception = (ObjectNullException) exceptionContext.Exception;
            var result = ResultModel<object>.Fail(exception);

            SetObjectResult(exceptionContext, result);
        }

        private void HandleNotFoundException(ExceptionContext exceptionContext)
        {
            var exception = (NotFoundException) exceptionContext.Exception;
            var result = ResultModel<object>.Fail(exception);

            SetObjectResult(exceptionContext, result);
        }

        private void HandleInvalidRequestException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is InvalidRequestException invalidRequestException)
            {
                var result = ResultModel<object>.Fail(invalidRequestException);

                SetObjectResult(exceptionContext, result);
            }

            if (exceptionContext.Exception is ValidationException validationException)
            {
                var errors = validationException
                    .Errors?
                    .Where(f => f != null)
                    .Select(d => new ErrorDetail(d.PropertyName, d.ErrorMessage))
                    .ToList();

                var result = errors != null
                    ? ResultModel<object>.Fail(new InvalidRequestException(info: errors))
                    : ResultModel<object>.Fail(new InvalidRequestException());

                SetObjectResult(exceptionContext, result);
            }

            exceptionContext.ModelState.Clear();
        }

        private void HandleInternalServerErrorException(ExceptionContext exceptionContext)
        {
            var exception = (InternalServerErrorException) exceptionContext.Exception;
            var result = ResultModel<object>.Fail(exception);

            result.Error?.AdditionalInfo.Add(new ErrorDetail("Exception Message", exception.Message));
            SetObjectResult(exceptionContext, result);
        }


        private void SetObjectResult(ExceptionContext exceptionContext, ResultModel<object> result)
        {
            exceptionContext.Result = new ObjectResult(result)
            {
                StatusCode = result.ErrorCode.ToStatusCode()
            };
        }
    }
}