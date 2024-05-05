using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CandidateJourney.API;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<CustomExceptionFilterAttribute> _logger;

    public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        /*var exception = context.Exception;
        var response = context.HttpContext.Response;

        ErrorMessageModel errorDto;
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errorDto = new ErrorMessageModel(statusCode, validationException.Errors.Select(x => x.ErrorMessage));
                errorDto.Exception = exception.InnerException?.Message;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorDto = new ErrorMessageModel(statusCode, exception.Message);
                errorDto.Exception = exception.InnerException?.Message;
                break;
        }
            
        context.ExceptionHandled = true;
        response.StatusCode = (int)statusCode;
        response.ContentType = "application/json";
        context.Result = new ObjectResult(errorDto);*/
    }
}