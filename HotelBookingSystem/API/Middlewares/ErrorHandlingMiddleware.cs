using HotelBookingSystem.Application.Helper;
using HotelBookingSystem.Application.Helper.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelBookingSystem.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Process the request
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Handle the exception by writing a custom response
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            bool success = false;

            string message = exception.Message == null ? "Internal Server Error" : exception.Message;
            int statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is BaseException)
            {
                success = ((BaseException)exception).Success;
                message = ((BaseException)exception).Message;
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception.Message == Constants.TOKENHASEXPIRED)
            {
                //todo have to change the exception instances   

                success = false;
                message = Constants.SESSIONHASEXPIRED;
                statusCode = (int)HttpStatusCode.Unauthorized;
            }

            string response = Serializer.Serialize(new { success = success, message = message }, false);
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json; charset=utf-8";

            await httpContext.Response.WriteAsync(response);
        }
    }
}
