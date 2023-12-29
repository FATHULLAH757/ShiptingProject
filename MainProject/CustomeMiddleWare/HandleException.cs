using MainProject.Config;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace MainProject.CustomeMiddleWare
{
    public class HandleException
    {
        private readonly RequestDelegate _next;
       // private readonly ILogger<ExceptionMiddle> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        public HandleException(
            RequestDelegate next,
           // ILogger<ExceptionMiddle> logger,
            IHostEnvironment hostEnvironment)
        {
            _next = next;
           // _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //var orignalbodyStream = httpContext.Response.Body;
                //await using var responseBody = new MemoryStream();
                //httpContext.Response.Body = responseBody;
                await _next.Invoke(httpContext);
                //httpContext.Response.Body.CanRead = true;

               // var response = await GetResponseAsText(httpContext.Response);

                // var bodyAsText = await new System.IO.StreamReader(httpContext.Response.Body).ReadToEndAsync();

                // await httpContext.Response.WriteAsync(text);
               // await responseBody.CopyToAsync(orignalbodyStream);

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _hostEnvironment.IsDevelopment() ?
                    new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new APIException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }


        }
        private async Task<string> GetResponseAsText(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
