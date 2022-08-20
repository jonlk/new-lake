internal static class ExceptionHandler
{
    internal static IApplicationBuilder AddExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = Text.Plain;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    await ErrorUtility.SetErrorResponse(exceptionHandlerPathFeature.Error, context);
                });
            });

            app.UseHsts();
        }
        return app;
    }  
}