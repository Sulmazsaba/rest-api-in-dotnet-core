using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json.Serialization;
using Sample.DbContexts;
using Sample.GraphQl;
using Sample.GraphQl.Mutations;
using Sample.GraphQl.Queries;
using Sample.Services;

namespace Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters()
                   .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    // create a problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);

                    // add additional info not added by default
                    problemDetails.Detail = "See the errors field for details.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    // find out which status code to use
                    var actionExecutingContext =
                          context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "One or more validation errors occurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more errors on input occurred.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddDbContext<SampleContext>(options =>
            {
                var conn = this.Configuration.GetConnectionString("Default");
                options.UseSqlServer(conn);
                //options.UseSqlite(@"FileName=Sample", options =>
                //{
                //    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                //});
            });
            services.AddScoped<IJobRepository, JobRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<CompanyQuery>();
            services.AddScoped<AppSchema>();
            services.AddScoped<CompanyMutation>();

            services.AddGraphQL().AddSystemTextJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Sample", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseGraphQL<AppSchema>();

            app.UseGraphQLGraphiQL("/ui/graphql");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
