using System;
using System.Reflection;

namespace Imgeneus.Core.Handler.Internal.Transformers
{
    internal class ParameterFactory : IParameterFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITypeActivatorCache typeActivatorCache;

        /// <summary>
        /// Creates a new <see cref="ParameterFactory"/> instance.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        /// <param name="typeActivatorCache">Type Activator cache.</param>
        public ParameterFactory(IServiceProvider serviceProvider, ITypeActivatorCache typeActivatorCache)
        {
            this.serviceProvider = serviceProvider;
            this.typeActivatorCache = typeActivatorCache;
        }

        /// <inheritdoc />
        public object Create(TypeInfo type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return this.typeActivatorCache.Create<object>(this.serviceProvider, type.AsType());
        }
    }
}
