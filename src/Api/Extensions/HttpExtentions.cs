namespace Api.Extensions
{
    public static class HttpExtentions
    {
        public static IApplicationBuilder UseCustomHttp(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            return app;
        }
    }
}
