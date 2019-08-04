using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Imgeneus.Core.Handler.Extensions
{
    internal static class ParameterInfoExtensions
    {
        /// <summary>
        /// Gets the default value of a <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="parameter">Parameter informations.</param>
        /// <returns>Default value.</returns>
        public static object GetParameterDefaultValue(this ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException();
            }

            if (parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }

            if (parameter.ParameterType.IsValueType)
            {
                return Activator.CreateInstance(parameter.ParameterType);
            }

            return null;
        }
    }
}
