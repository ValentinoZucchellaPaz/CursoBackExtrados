﻿using MySqlConnector;
using System.Net;

namespace APITorneo.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MySqlException e)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Error conectandose a la base de datos", e);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Error inesperado", e);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message, Exception exception)
        {
            // Configura el código de estado HTTP y contenido de respuesta
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message,
                ExceptionType = exception.GetType(),
                ExceptionMessage = exception.Message
            };

            // Devuelve un JSON con el mensaje de error
            return context.Response.WriteAsync(response.ToString());
        }
    }
}