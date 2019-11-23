namespace Pokemon.Api.Core.Contracts
{
    public interface IGenericApiResponse<T>
    {
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int? ErrorNumber { get; set; }
        T Data { get; set; }
    }
}