using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace MusicControlBack
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection _)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMediaCommandHandler();
            app.Run(async (context) => 
            {
                context.Items.TryGetValue(Constants.SuccessFlag, out object commandExecuted);
                if (commandExecuted as string is null)
                {
                    await context.Response.WriteAsync("no command specified");
                }
                else
                {
                    await context.Response.WriteAsync($"pressed {commandExecuted as string ?? "<null>"}");
                }
            });
        }
    }
}
