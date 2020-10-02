﻿//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using FluentValidation;
//using FluentValidation.Results;
//using MediatR;

//namespace Realtor.Application.Behaviors
//{
//    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//    {
//        private readonly IEnumerable<IValidator<TRequest>> _validators;

//        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
//        {
//            _validators = validators;
//        }

//        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
//            RequestHandlerDelegate<TResponse> next)
//        {
//            ValidationContext context = new ValidationContext(request);
//            List<ValidationFailure> failures = _validators.Select(validator => validator.Validate(context))
//                .SelectMany(result => result.Errors)
//                .Where(failure => failure != null)
//                .ToList();

//            if (failures.Any())
//            {
//                throw new ValidationException(failures);
//            }

//            return next();
//        }
//    }
//}

//TODO: RT - Validators aren't working; will need to research. 