using System.Reflection;

namespace Imgeneus.Core.Handler.Internal.Transformers
{
    internal class ParameterTransformer : IParameterTransformer
    {
        private readonly ParameterTransformerCache transformerCache;

        /// <summary>
        /// Creates a new <see cref="ParameterTransformer"/> instance.
        /// </summary>
        /// <param name="transformerCache">Parameter transformer cache.</param>
        public ParameterTransformer(ParameterTransformerCache transformerCache)
        {
            this.transformerCache = transformerCache;
        }

        /// <inheritdoc />
        public object Transform(object originalParameter, TypeInfo destinationParameterType)
        {
            ParameterTransformerCacheEntry transformer = this.transformerCache.GetTransformer(destinationParameterType);

            if (transformer == null)
                return null;

            object destinationParameter = transformer.ParameterFactory(destinationParameterType);

            return transformer.Transformer(originalParameter, destinationParameter);
        }
    }
}
