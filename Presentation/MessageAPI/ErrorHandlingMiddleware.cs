using MessageService.Interfaces.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MessageAPI
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Entering {0}({1})",context.Request.Method, context.Request.Path);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
            _logger.LogInformation("Exiting {0}({1})({2})", context.Request.Method, context.Request.Path, context.Response.StatusCode);

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is CustomException) code = HttpStatusCode.BadRequest;

            // Using RFC 7807 response for error formatting
            // https://tools.ietf.org/html/rfc7807
            var problem = new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = exception.Message,
                Instance = "",
                Status = (int)code
            };

            var result = JsonConvert.SerializeObject(
                problem,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
