using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Filters
{
    public class ResponseWrapperFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is not ProblemDetails)
            {
                var response = new ApiResponse<object>
                {
                    Success = true,
                    Data = objectResult.Value
                };
                objectResult.Value = response;
            }

            await next();
        }
    }
}
