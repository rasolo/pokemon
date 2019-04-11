namespace Pokemon.Api.Core.Models
{
    public class GenericApiResponse<T>
    {
        public GenericApiResponse(T data = default(T), string errorMessage = null, int? errorNumber = null)
        {
            Data = data;
            ErrorMessage = errorMessage;
            ErrorNumber = errorNumber;
            Success = errorMessage == null;
        }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int? ErrorNumber { get; set; }
        public T Data { get; set; }
    }
}
