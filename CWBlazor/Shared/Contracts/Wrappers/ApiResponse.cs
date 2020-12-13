namespace CWBlazor.Shared.Contracts.Wrappers
{
    /// <summary>
    /// Ok response wrapper class.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="obj">Object to wrap.</param>
        public ApiResponse(object obj)
        {
            Data = obj;
        }

        /// <summary>
        /// Wrapped data.
        /// </summary>
        public object Data { get; set; }
    }
}
