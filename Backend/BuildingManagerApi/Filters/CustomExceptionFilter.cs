using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidArgumentException || context.Exception is ArgumentException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 400
                };
            }
            else if (context.Exception is DuplicatedValueException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 400
                };
            }
            else if (context.Exception is NotFoundException || context.Exception is ValueNotFoundException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 404
                };
            }
            else if (context.Exception is InvalidOperationException)
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