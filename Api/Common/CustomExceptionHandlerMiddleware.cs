﻿using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var errorResult = string.Empty;

            switch (ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    errorResult = System.Text.Json.JsonSerializer.Serialize(new { errores = validationException.Failures });
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    errorResult = System.Text.Json.JsonSerializer.Serialize(new ErrorModel { Message = badRequestException.Message, Hint = ex.Source, Stack = ex.StackTrace });
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    errorResult = System.Text.Json.JsonSerializer.Serialize(new ErrorModel { Message = "NotFoundException", Hint = ex.Source, Stack = ex.StackTrace });
                    break;
                case TokenResourceException tokenResourceException:
                    code = HttpStatusCode.UnprocessableEntity;
                    errorResult = System.Text.Json.JsonSerializer.Serialize(new ErrorModel { Message = tokenResourceException.Message, Hint = ex.Source, Stack = ex.StackTrace });
                    break;
            } //422

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (errorResult == string.Empty)
            {
                errorResult = System.Text.Json.JsonSerializer.Serialize(new ErrorModel { Message = ex.Message, Hint = ex.Source, Stack = ex.StackTrace });
            }

            return context.Response.WriteAsync(errorResult);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
