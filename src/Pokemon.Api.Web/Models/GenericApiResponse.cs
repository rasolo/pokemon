using Pokemon.Api.Core.Contracts;

namespace Pokemon.Api.Web.Models
{
    public class GenericApiResponse<T> : IGenericApiResponse<T>
    {
        public GenericApiResponse(T data = default, string errorMessage = null, int? errorNumber = null)
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