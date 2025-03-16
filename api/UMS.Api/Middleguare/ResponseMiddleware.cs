using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace UMS.Api.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(httpContext);
                return;
            }

            var originalBodyStream = httpContext.Response.Body;
            using (var newBodyStream = new MemoryStream())
            {
                httpContext.Response.Body = newBodyStream;

                try
                {
                    await _next(httpContext);

                    if (httpContext.Response.StatusCode == (int)HttpStatusCode.NoContent)
                    {
                        httpContext.Response.Body = originalBodyStream;
                        return;
                    }

                    newBodyStream.Seek(0, SeekOrigin.Begin);
                    string responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();

                    var response = new ApiResponse
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        Message = httpContext.Response.StatusCode == (int)HttpStatusCode.OK ? "Successful Request" : "Request Failed",
                        Data = null
                    };

                    if (!string.IsNullOrWhiteSpace(responseBody) &&
                        httpContext.Response.ContentType != null &&
                        httpContext.Response.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            response.Data = JsonConvert.DeserializeObject<object>(responseBody);
                        }
                        catch (JsonException)
                        {
                            response.Data = responseBody; // Si hay error de JSON, al menos devolvemos el texto
                        }
                    }

                    if (httpContext.Response.StatusCode >= 400)
                    {
                        response.Data = await GetErrorDetails(httpContext);
                    }

                    string responseJson = JsonConvert.SerializeObject(response);

                    // Restauramos el stream original
                    httpContext.Response.Body = originalBodyStream;
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(responseJson);
                    await httpContext.Response.WriteAsync(responseJson);
                }
                catch (Exception ex)
                {
                    var errorResponse = new ApiResponse
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = "An internal server error occurred.",
                        Data = new
                        {
                            ErrorMessage = ex.Message,
                            StackTrace = ex.StackTrace
                        }
                    };

                    string responseJson = JsonConvert.SerializeObject(errorResponse);
                    httpContext.Response.Body = originalBodyStream;
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(responseJson);
                    await httpContext.Response.WriteAsync(responseJson);
                }
            }
        }

        private async Task<object> GetErrorDetails(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return new { Error = "Bad request: Please check your input." };
            }
            return new { Error = "An unknown error occurred." };
        }

        public class ApiResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }
    }
}
