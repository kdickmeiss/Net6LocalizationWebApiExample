using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplicationExample;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Example Web application API",
                Version = "v1"
            });

            c.OperationFilter<AcceptedLanguageHeader>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example Web application API v1");
        });
    }
}

public class AcceptedLanguageHeader : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language",
            Description = "Accepted language value (en-US, de-DE)",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema()
            {
                Type = "string",
                Default = new OpenApiString("en-US")
            },
            Required = true,
        });
    }
}
