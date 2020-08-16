using ContactAddress.Application.Core.Commands;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAddress.Application.Core.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(async v => await v.ValidateAsync(context, cancellationToken))
                .Select(t => t.Result)
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .Select(x => new ValidationError { PropertyName = x.PropertyName, Message = x.ErrorMessage })
                .ToList();

            if (failures.Any())
            {
                var responseType = typeof(TResponse);
                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(BaseResponse<>).MakeGenericType(resultType);
                dynamic invalidResponse = Activator.CreateInstance(invalidResponseType, null);

                invalidResponse.Errors = failures;
                return invalidResponse as TResponse;

            }
            else
            {

                return await next();
            }
        }
    }
}
