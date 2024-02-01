using Api.Infra;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "iParty API",
                    Description = "This is an API for a future mobile app called iParty. The purpose of the app is to connect event sector suppliers to people who want to promote an event like weddings, anniversaries, and ceremonies in general.",
                    Contact = new OpenApiContact
                    {
                        Name = "Haiti Naspolini Neto e Oséias da Silva Martins",
                        Url = new Uri("https://github.com/ozmartins/iParty"),
                        Email = "haitineto@gmail.com;oseias.silva.martins@gmail.com"
                    },
                });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iParty.Api v1"));

            var option = new RewriteOptions();

            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            return app;
        }
    }
}
