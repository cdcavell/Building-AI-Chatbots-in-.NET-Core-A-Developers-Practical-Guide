using AI.Chatbot.Models.AppSettings;
using ClassLibrary.Mvc.Exceptions.Handlers;
using ClassLibrary.Mvc.Services.AppSettings;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

namespace AI.Chatbot.Extensions
{
    internal static class WebApplicationExtensions
    {
        private static AppSettings? _appSettings;

        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, lc) => lc
                .ReadFrom.Configuration(ctx.Configuration));

            _appSettings = new(builder.Configuration);
            builder.Services.AddAppSettingsService(options =>
            {
                options.AppSettings = _appSettings;
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddResponseCaching();

            // Exception Handlers are called in the order they are registered
            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddExceptionHandler<ArgumentExceptionHandler>();
            builder.Services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
            builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePages(subApp =>
            {
                subApp.Run(async context =>
                {
                    int statusCode = context.Response.StatusCode;
                    context.Response.Redirect($"/Status/{statusCode}");
                    await context.Response.StartAsync().ConfigureAwait(false);
                });
            });

            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            return app;
        }
    }
}
