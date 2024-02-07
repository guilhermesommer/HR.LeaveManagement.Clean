using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Net;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;

                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = notFound.Message,
                        Status = (int)statusCode,
                        Detail = notFound.InnerException?.Message,
                        Type = nameof(NotFoundException)
                    };
                    break;

                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError)
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
