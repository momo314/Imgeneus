using Imgeneus.Core.Handler.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Imgeneus.Core.Handler.Internal
{
    internal class HandlerActionCache : IHandlerActionCache
    {
        private readonly IDictionary<object, HandlerActionModel> handlerCache;

        /// <summary>
        /// Creates a new <see cref="HandlerActionCache"/> instance.
        /// </summary>
        /// <param name="cacheEntries">Cached entries.</param>
        public HandlerActionCache(IDictionary<object, HandlerActionModel> cacheEntries)
        {
            this.handlerCache = new ConcurrentDictionary<object, HandlerActionModel>(cacheEntries);
        }

        /// <inheritdoc />
        public HandlerActionModel GetHandlerAction(object handlerAction)
        {
            return this.handlerCache.TryGetValue(handlerAction, out HandlerActionModel value) ? value : null;
        }
    }
}
