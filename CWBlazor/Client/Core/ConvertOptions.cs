using System;
using System.Collections.Generic;

namespace CWBlazor.Client.Core
{
    /// <summary>
    /// Options how to convert object to collection of <see cref="KeyValuePair"/>.
    /// </summary>
    [Flags]
    public enum ConvertOptions
    {
        /// <summary>
        /// Indicates that no options should be applied during conversion.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that properties with null values should be excluded.
        /// </summary>
        ExcludeNulls = 2,

        /// <summary>
        /// Indicates that string properties with empty values should be excluded.
        /// </summary>
        ExcludeEmptyStrings = 4,

        /// <summary>
        /// Indicates that dates should be converted to a formatted string (Iso8601).
        /// </summary>
        ConvertDatesToIso8601 = 8,

        /// <summary>
        /// Indicates that all options should be applied.
        /// </summary>
        All = ExcludeNulls | ExcludeEmptyStrings | ConvertDatesToIso8601,
    }
}
