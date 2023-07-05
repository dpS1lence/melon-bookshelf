using MelonBookshelfApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MelonBookshelfApi.ApplicationExtentions
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Adds Swagger and Swagger UI with Swagger Settings
        /// </summary>
        /// <param name="builder">Application Builder</param>
        /// <param name="swaggerSettings">Swagger Settings</param>
        public static void UseSwagger(this IApplicationBuilder builder, SwaggerSettings swaggerSettings)
        {
            if (swaggerSettings == null)
            {
                throw new ArgumentNullException(nameof(swaggerSettings));
            }

            swaggerSettings.ValidateAndThrow();

            var options = new SwaggerUIOptions
            {
                DocumentTitle = swaggerSettings.ApiName
            };

            if (!string.IsNullOrEmpty(swaggerSettings.RoutePrefix))
            {
                options.RoutePrefix = swaggerSettings.RoutePrefix;
            }

            builder.UseSwagger(p =>
            {
                p.RouteTemplate = swaggerSettings.JsonRoute;
            });

            options.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.ApiName);
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);

            builder.UseSwagger(p => p.RouteTemplate = swaggerSettings.JsonRoute);
            builder.UseSwaggerUI(p =>
            {
                p.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.ApiName);
                p.DocExpansion(DocExpansion.None);
                p.DisplayRequestDuration();
                p.DocumentTitle = swaggerSettings.ApiName;
                p.RoutePrefix = swaggerSettings.RoutePrefix;
            });

            builder.UseMiddleware<SwaggerUIMiddleware>(options);
        }
    }
}
