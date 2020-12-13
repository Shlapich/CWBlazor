using System.Collections.Generic;

namespace CWBlazor.Shared.Contracts.Wrappers
{
    /// <summary>
    /// Error model.
    /// </summary>
    public class ErrorsModel
    {
        /// <summary>
        /// Global(summary) errors collection.
        /// </summary>
        public IEnumerable<string> SummaryErrors { get; set; }

        /// <summary>
        /// Parameterized collection.
        /// </summary>
        public IList<ParameterizedError> InvalidParams { get; set; }
    }
}
