using Imgeneus.Core.Handler.Extensions;
using Imgeneus.Core.Handler.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Imgeneus.Core.Handler.Internal
{
    internal sealed class HandlerActionInvokerCache : IDisposable
    {
        private readonly IDictionary<object, HandlerActionInvokerCacheEntry> cache;
        private readonly IHandlerActionCache handlerCache;
        private readonly IHandlerFactory handlerFactory;

        /// <summary>
        /// Creates a new <see cref="HandlerActionInvokerCache"/> instance.
        /// </summary>
        /// <param name="handlerCache">Handler action cache.</param>
        /// <param name="handlerFactory">Handler factory.</param>
        public HandlerActionInvokerCache(IHandlerActionCache handlerCache, IHandlerFactory handlerFactory)
        {
            this.cache = new ConcurrentDictionary<object, HandlerActionInvokerCacheEntry>();
            this.handlerCache = handlerCache;
            this.handlerFactory = handlerFactory;
        }

        /// <summary>
        /// Gets a <see cref="HandlerActionInvokerCacheEntry"/> based on a handler action.
        /// </summary>
        /// <remarks>
        /// <see cref="HandlerActionInvokerCacheEntry"/> contains the handler factory creator 
        /// and the handler action executor.
        /// </remarks>
        /// <param name="handlerAction">Handler action.</param>
        /// <returns>
        /// Existing <see cref="HandlerActionInvokerCacheEntry"/> in cache; 
        /// if the entry doesn't exist, it creates a new <see cref="HandlerActionInvokerCacheEntry"/>, 
        /// caches it and returns it.
        /// </returns>
        public HandlerActionInvokerCacheEntry GetCachedHandlerAction(object handlerAction)
        {
            if (!this.cache.TryGetValue(handlerAction, out HandlerActionInvokerCacheEntry cacheEntry))
            {
                HandlerActionModel handlerActionModel = this.handlerCache.GetHandlerAction(handlerAction);

                if (handlerActionModel == null)
                {
                    throw new ArgumentNullException(nameof(handlerActionModel));
                }

                object[] defaultHandlerActionParameters = handlerActionModel.Method.GetMethodParametersDefaultValues();

                cacheEntry = new HandlerActionInvokerCacheEntry(
                    handlerActionModel.HandlerTypeInfo.AsType(),
                    this.handlerFactory.CreateHandler,
                    this.handlerFactory.ReleaseHandler,
                    new HandlerExecutor(handlerActionModel.HandlerTypeInfo, handlerActionModel.Method, defaultHandlerActionParameters));

                this.cache.Add(handlerAction, cacheEntry);
            }

            return cacheEntry;
        }

        /// <summary>
        /// Dispose the <see cref="HandlerActionInvokerCache"/> resources.
        /// </summary>
        public void Dispose()
        {
            this.cache.Clear();
        }
    }
}
