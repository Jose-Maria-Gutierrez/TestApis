﻿using System.Net;
using System.Text.Json;

namespace TestApis.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); //llama el prox middleware
            }catch (Exception error) //cachea errores del middleware
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {message = error?.Message});
                await response.WriteAsync(result);
            }
        }

    }
}
