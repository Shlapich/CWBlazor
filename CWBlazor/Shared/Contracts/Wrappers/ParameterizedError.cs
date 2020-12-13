using System.Collections.Generic;

namespace CWBlazor.Shared.Contracts.Wrappers
{
    /// <summary>
    /// Parameterized error model.
    /// </summary>
    public class ParameterizedError
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter errors collection.
        /// </summary>
        public IEnumerable<string> Reason { get; set; }
    }
}
