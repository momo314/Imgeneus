using System;

namespace Imgeneus.Core.Handler.Internal
{
    /// <summary>
    /// Provides methods to create and release handlers.
    /// </summary>
    internal sealed class HandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITypeActivatorCache typeActivatorCache;

        /// <summary>
        /// Creates a new <see cref="HandlerFactory"/> instance.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="typeActivatorCache"></param>
        public HandlerFactory(IServiceProvider serviceProvider, ITypeActivatorCache typeActivatorCache)
        {
            this.serviceProvider = serviceProvider;
            this.typeActivatorCache = typeActivatorCache;
        }

        /// <inheritdoc />
        public object CreateHandler(Type handlerType)
        {
            if (handlerType == null)
                throw new ArgumentNullException(nameof(handlerType));

            return this.typeActivatorCache.Create<object>(this.serviceProvider, handlerType);
        }

        /// <inheritdoc />
        public void ReleaseHandler(object handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            if (handler is IDisposable disposableHandler)
                disposableHandler.Dispose();
        }
    }
}
