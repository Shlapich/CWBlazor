using CWBlazor.Shared.Contracts.Wrappers;

namespace CWBlazor.Client
{
    public class UniversalResponse<T>
    {
        public T Value { get; set; }

        public ErrorResponse Error { get; set; }

        public bool IsSuccess { get; set; }
    }
}