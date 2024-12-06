namespace Application.Struct
{
    public class ValidationResult<DType>
    {
        public DType? Data { get; set; }
        public bool IsValid { get; set; }
        public Exception? Error { get; set; }

        public string Message => Error?.Message ?? "Unknown error";
        public DType Value => Data ?? throw new InvalidOperationException("Data is null");

        public ValidationResult(DType? data, bool isValid, Exception? message)
        {
            Data = data;
            IsValid = isValid;
            Error = message;
        }
    }
}