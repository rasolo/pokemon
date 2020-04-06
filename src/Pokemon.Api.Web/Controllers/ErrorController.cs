using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Core.Exceptions;
using Pokemon.Api.Core.Extensions;
using Pokemon.Api.Core.Logging;
using Pokemon.Api.Web.Models;

namespace Pokemon.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : Controller
    {
        private readonly ILoggingService _loggingService;

        public ErrorController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public GenericApiResponse<string> Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception == null)
            {
                return new GenericApiResponse<string>(null, ApiErrors.UnknownError.GetDescription());
            }

            var apiException = exception as ApiException;
            var errorNumber = apiException?.ErrorNumber ?? null;
            _loggingService.Error(
                $"Request failed: {exception.HResult} {exception.Message} | Error Number: {errorNumber} |");

            return new GenericApiResponse<string>(null, exception.Message, errorNumber);
        }
    }
}