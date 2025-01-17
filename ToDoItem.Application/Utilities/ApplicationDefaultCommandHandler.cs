using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.Messages;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Sahred.Dtos;
using ToDoItem.Sahred.Utilites;

namespace ToDoItem.Application.Utilities
{
    public abstract class ApplicationDefaultCommandHanlder<TRequest, TResponse> : IApplicationRequestHandler<TRequest, ResponseDto<TResponse>>
        where TRequest : IApplicationRequest<ResponseDto<TResponse>>
    {
        private readonly ILogger<object> _logger;
        private readonly IValidator<TRequest> _validator;

        protected ApplicationDefaultCommandHanlder(ILogger<object> logger, IValidator<TRequest> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<ResponseDto<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var resposne = new ResponseDto<TResponse>();
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                return await ExecutAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, ApplicationMessages.HANDLEREXECUTION_EXCEPTION);
                return resposne.OnException(ApplicationMessages.HANDLEREXECUTION_EXCEPTION, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ApplicationMessages.HANDLEREXECUTION_EXCEPTION);
                return resposne.OnException(ApplicationMessages.HANDLEREXECUTION_EXCEPTION, ex);
            }
        }

        protected abstract Task<ResponseDto<TResponse>> ExecutAsync(TRequest request, CancellationToken cancellationToken);
    }
}
