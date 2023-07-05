using MelonBookshelfApi.Swagger;
using Microsoft.OpenApi.Models;

namespace MelonBookshelfApi.ProgramExtentions
{
    public static class SwaggerConfigurationExtentions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerConfiguration> configuration)
        {
            var swaggerConfig = new SwaggerConfiguration();
            configuration(swaggerConfig);

            var swaggerSettings = swaggerConfig.Settings;

            services.AddSwaggerFromConfiguration(swaggerSettings);
            return services;
        }

        private static void AddSwaggerFromConfiguration(this IServiceCollection services, SwaggerSettings settings)
        {
            settings.ValidateAndThrow();
            var apiVersion = settings.ApiVersionText;

            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc(apiVersion, new OpenApiInfo()
                {
                    Title = settings.ApiName,
                    Version = apiVersion
                });
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
