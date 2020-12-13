using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CWBlazor.Shared.Contracts.Wrappers
{
    /// <summary>
    /// Error response wrapper.
    /// </summary>
    public class ErrorResponse
    {
        [JsonConstructor]
        public ErrorResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="errorsModel">Errors model.</param>
        public ErrorResponse(ErrorsModel errorsModel)
        {
            Errors = errorsModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="globalErrors">Global errors messages collection.</param>
        public ErrorResponse(IEnumerable<string> globalErrors)
        {
            Errors = new ErrorsModel { SummaryErrors = globalErrors };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="globalError">Global error message.</param>
        public ErrorResponse(string globalError)
        {
            Errors = new ErrorsModel { SummaryErrors = new[] { globalError } };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="invalidParams">Invalid params collection.</param>
        public ErrorResponse(IEnumerable<ParameterizedError> invalidParams)
        {
            Errors = new ErrorsModel { InvalidParams = invalidParams.ToList() };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="globalErrors">Global errors messages collection.</param>
        /// <param name="invalidParams">Invalid params collection.</param>
        public ErrorResponse(IEnumerable<string> globalErrors, IEnumerable<ParameterizedError> invalidParams)
        {
            Errors = new ErrorsModel { SummaryErrors = globalErrors, InvalidParams = invalidParams.ToList() };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="globalError">Global error message.</param>
        /// <param name="invalidParams">Invalid params collection.</param>
        public ErrorResponse(string globalError, IEnumerable<ParameterizedError> invalidParams)
        {
            Errors = new ErrorsModel { SummaryErrors = new[] { globalError }, InvalidParams = invalidParams.ToList() };
        }

        /// <summary>
        /// Errors model.
        /// </summary>
        public ErrorsModel Errors { get; set; }
    }
}
