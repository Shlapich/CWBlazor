using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CWBlazor.Client.Core;
using CWBlazor.Shared;

namespace CWBlazor.Client.Exceptions
{
    /// <summary>
    /// Extends <see cref="object"/> with custom methods.
    /// </summary>
    public static class ObjectX
    {
        /// <summary>
        /// Converts object to a collection of key-value parameters
        /// where key is a property name and value is a property value.
        /// </summary>
        /// <param name="dto">Plain data transfer object to be converted.</param>
        /// <returns>Collection of <see cref="KeyValuePair"/> items.</returns>
        public static IEnumerable<KeyValuePair<string, object>> ConvertToKeyValuePairCollection(this object dto)
        {
            return dto.ConvertToDictionary().AsEnumerable();
        }

        /// <summary>
        /// Converts object to a dictionary
        /// where key is a property name and value is a property value.
        /// </summary>
        /// <param name="dto">Plain data transfer object to be converted.</param>
        /// <returns>The <see cref="IReadOnlyDictionary{TKey,TValue}"/>.</returns>
        public static IReadOnlyDictionary<string, object> ConvertToDictionary(this object dto)
        {
            const BindingFlags bindingAttr = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance;

            return dto.GetType().GetProperties(bindingAttr)
                .ToDictionary(propInfo => propInfo.Name, propInfo => propInfo.GetValue(dto, null));
        }

        /// <summary>
        /// Converts object to a collection of key-value parameters
        /// where key is a property name and value is a property value.
        /// </summary>
        /// <param name="dto">Plain data transfer object to be converted.</param>
        /// <param name="convertOptions">
        /// Specific options applied during conversion. <seealso cref="ConvertOptions"/>.
        /// </param>
        /// <returns>Collection of <see cref="KeyValuePair"/> items.</returns>
        public static IEnumerable<KeyValuePair<string, object>> ConvertDtoToDictionaryOfParameters(
            this object dto,
            ConvertOptions convertOptions = ConvertOptions.All)
        {
            var parameters = dto.ConvertToKeyValuePairCollection();

            if ((convertOptions & ConvertOptions.ExcludeNulls) != 0)
            {
                parameters = parameters.Where(item => item.Value != null);
            }

            if ((convertOptions & ConvertOptions.ExcludeEmptyStrings) != 0)
            {
                parameters = parameters.Where(item => !(item.Value is string value && string.IsNullOrEmpty(value)));
            }

            var resultParams = new Dictionary<string, object>();

            foreach (var (key, value) in parameters)
            {
                var resultValue = value;
                if ((convertOptions & ConvertOptions.ConvertDatesToIso8601) != 0 && value is DateTime dateTime)
                {
                    resultValue = dateTime.ToString(Constants.WebFormats.Iso8601DateFormat);
                }

                resultParams[key] = resultValue;
            }

            return resultParams;
        }

        /// <summary>
        /// Convert object to decimal.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Decimal.</returns>
        public static decimal ConvertToDecimal(this object obj)
        {
            var str = obj.ToString();

            return Convert.ToDecimal(str);
        }

        /// <summary>
        /// Convert object to decimal.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Decimal.</returns>
        public static decimal? ConvertToNullableDecimal(this object obj)
        {
            return obj?.ConvertToDecimal();
        }
    }
}
