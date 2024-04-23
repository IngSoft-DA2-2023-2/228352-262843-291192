using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerModels.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace ECommerceApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidArgumentException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 400
                };
            }
            else if (context.Exception is ValueDuplicatedException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 400
                };
            }
            else if (context.Exception is Exception)
            {
                context.Result = new ObjectResult(new { ErrorMessage = $"Something went wrong. See: {context.Exception.Message}" })
                {
                    StatusCode = 500
                };
            }
        }

    }
}