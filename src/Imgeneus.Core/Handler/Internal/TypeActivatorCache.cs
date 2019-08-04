using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Imgeneus.Core.Handler.Internal
{
    /// <summary>
    /// Caches <see cref="ObjectFactory"/> instances produced by <see cref="ActivatorUtilities.CreateFactory(Type, Type[])"/>.
    /// </summary>
    internal interface ITypeActivatorCache
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TInstance"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> used to resolve dependencies for
        /// <paramref name="optionType"/>.</param>
        /// <param name="optionType">The <see cref="Type"/> of the <typeparamref name="TInstance"/> to create.</param>
        TInstance Create<TInstance>(IServiceProvider serviceProvider, Type implementationType) where TInstance : class;
    }

    /// <summary>
    /// Caches <see cref="ObjectFactory"/> instances produced by <see cref="ActivatorUtilities.CreateFactory(Type, Type[])"/>.
    /// </summary>
    internal class TypeActivatorCache : ITypeActivatorCache
    {
        private readonly Func<Type, ObjectFactory> _createFactory;
        private readonly ConcurrentDictionary<Type, ObjectFactory> _typeActivatorCache;

        /// <summary>
        /// Creates a new <see cref="TypeActivatorCache"/>.
        /// </summary>
        public TypeActivatorCache()
        {
            this._createFactory = (type) => ActivatorUtilities.CreateFactory(type, Type.EmptyTypes);
            this._typeActivatorCache = new ConcurrentDictionary<Type, ObjectFactory>();
        }

        /// <inheritdoc />
        public TInstance Create<TInstance>(IServiceProvider serviceProvider, Type implementationType) where TInstance : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            ObjectFactory factory = this._typeActivatorCache.GetOrAdd(implementationType, this._createFactory);

            return (TInstance)factory(serviceProvider, null);
        }
    }
}
