using System;
using Pokemon.Api.Core.Extensions;

namespace Pokemon.Api.Core.Exceptions
{
    public class ApiException : Exception
    {
        public int ErrorNumber { get; set; }

        public ApiException(ApiErrors error, Exception innerException = null) : base(error.GetDescription(), innerException)
        {
            ErrorNumber = (int)error;
        }
    }
}
