using System;
using Catalog.Domain.Shared;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Catalog.Api
{
    public class GrpcGlobalExceptionHandlerInterceptor : Interceptor
    {
        private readonly ILogger<GrpcGlobalExceptionHandlerInterceptor> _logger;

        public GrpcGlobalExceptionHandlerInterceptor(ILogger<GrpcGlobalExceptionHandlerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await base.UnaryServerHandler(request, context, continuation);
            }
            catch (Exception ex)
            {
                if (ex is RpcException)
                    throw;
                else
                    throw new RpcException(new Status(StatusCode.Internal, "Internal Error. Contact Admin."));
            }
        }
    }
}