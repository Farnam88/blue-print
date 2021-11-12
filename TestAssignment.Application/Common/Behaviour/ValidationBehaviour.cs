using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Exceptions;

namespace TestAssignment.Application.Common.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .Select(d => new ErrorDetail(d.PropertyName, d.ErrorMessage))
                    .ToList();
                
                if (failures.Count != 0)
                    throw new InvalidRequestException(info: failures);
            }

            return await next();
        }
    }
}