
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.AspNetCore;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Security;

namespace MessageAPI.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Messages API";
                    document.Info.Description = "Control and create email, sms and telegram messages";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Mitja Santl",
                        Email = "mitjasantl@gmail.com"
                    };
                    document.Info.License = new SwaggerLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
            });
        }

    }
}
