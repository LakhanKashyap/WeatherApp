using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Net;
using WeatherApp.Api.DTOs;

namespace WeatherApp.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "External weather API request failed.");

                context.Response.StatusCode = (int)HttpStatusCode.BadGateway;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Weather service error.",
                    Message = "The external weather service could not be reached."
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to save weather data to the database.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Database error.",
                    Message = "The weather data could not be saved."
                });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database operation failed.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Database error.",
                    Message = "The database operation could not be completed."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Error = "Internal server error.",
                    Message = "An unexpected error occurred."
                });
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}