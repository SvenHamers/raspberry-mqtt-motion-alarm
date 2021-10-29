using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace raspberry_mqqt_motion_alarm.Atributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "x-api-key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
          
                };
                return;
            }

            var apiKey = Program.appSettings.Api_Password;

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
  
                };
                return;
            }

            await next();
        }
    }
}
