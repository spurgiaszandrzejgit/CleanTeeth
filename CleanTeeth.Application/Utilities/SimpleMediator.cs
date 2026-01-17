using CleanTeeth.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Application.Utilities
{
    internal class SimpleMediator : IMediator
    {
        private readonly IServiceProvider serviceProvider;
        public SimpleMediator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = serviceProvider.GetService(handlerType);

            if (handler == null)
            {
                throw new MediatorException($"Handler was not found for {request.GetType().Name}");
            }

            var method = handlerType.GetMethod("Handle");
            return await (Task<TResponse>)method!.Invoke(handler, new object[] { request })!;
        }
    }
}
