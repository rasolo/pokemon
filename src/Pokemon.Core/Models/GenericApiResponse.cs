namespace Pokemon.Api.Core.Models
{
    public class GenericApiResponse<T>
    {
        public GenericApiResponse(T data = default(T), string errorMessage = null)
        {
            Data = data;
            ErrorMessage = errorMessage;
            Success = errorMessage == null;
        }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
