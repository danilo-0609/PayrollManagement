using ErrorOr;
using FluentValidation;
using MediatR;

namespace PayrollManagement.Users.Application.Common.Behavior
{
    public sealed class ValidatorBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {

        private readonly IValidator<TRequest> _validator;

        public ValidatorBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors
                                .ConvertAll(validationFalure => Error.Validation(
                                    validationFalure.PropertyName,
                                    validationFalure.ErrorMessage
                                    ));

            return (dynamic)errors;
        }
    }
}
