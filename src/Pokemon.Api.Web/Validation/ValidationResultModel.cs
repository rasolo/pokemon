using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pokemon.Api.Core.Contracts;
using Pokemon.Api.Core.Exceptions;
using Pokemon.Api.Core.Extensions;
using System.Collections.Generic;
using System.Linq;


namespace Pokemon.Api.Web.Validation
{
    public class ValidationResultModel<T> : IGenericApiResponse<List<ValidationError>>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int? ErrorNumber { get; set; }
        public List<ValidationError> Data { get; set; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Success = false;
            ErrorMessage = ApiErrors.ValidationFailed.GetDescription();
            ErrorNumber = (int)ApiErrors.ValidationFailed;
            Data = modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
            .ToList();
        }
    }
}
