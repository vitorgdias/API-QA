using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Options;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace APIqa
{
    public static class Doc
    {
        /// <summary>
        /// Adiciona a documentação no container de injeção de dependência
        /// </summary>
        /// <param name="services">Container de injeção de dependência</param>
        public static void AddDocumentations(this IServiceCollection services, IConfiguration configuration)
        {
            var apiOptions = configuration.GetSection("ApiDocumentation");
            services.Configure<ApiDocumentationOptions>(apiOptions);
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "nome",
                        Description = "testes qa",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "seu nome",
         
                            Email = "email@emial.com"
                        }
                    });
                config.DescribeAllParametersInCamelCase();
                config.CustomSchemaIds(x => x.FullName);
                config.OrderActionsBy(x => x.GroupName);
            });
        }
        /// <summary>
        /// Adiciona o middleware para exibição da documentação das APIs
        /// </summary>
        /// <param name="app"></param>
        public static void UseDocumentations(this IApplicationBuilder app)
        {
            var apiOptions = app.ApplicationServices.GetService<IOptions<ApiDocumentationOptions>>().Value;
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.DocumentTitle = apiOptions.AppName;
                config.SwaggerEndpoint(apiOptions.DocJson, apiOptions.AppName);
                config.RoutePrefix = apiOptions.DocRoute;
                config.DisplayRequestDuration();
                config.DocExpansion(DocExpansion.None);
                config.EnableValidator();
            });
        }
        public class ApiDocumentationOptions

        {

            public string AppName { get; set; }

            public string Description { get; set; }

            public string Owner { get; set; }

            public string Email { get; set; }

            public string Url { get; set; }

            public string DocJson { get; set; }

            public string DocRoute { get; set; }

        }
    }
}
