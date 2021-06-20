using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared;
using static System.Console;
using NorthwindService.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NorthwindService
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
            string databasePath = Path.Combine("..", "Northwind.db");
            services.AddDbContext<Northwind>(options=>options.UseSqlite($"Data Source = {databasePath}"));

            services.AddControllers(options=>
            {
                WriteLine("Default output formatters:");
                foreach(IOutputFormatter formatter in options.OutputFormatters) {
                    var mediaFormatter = formatter as OutputFormatter;
                    if (mediaFormatter == null) {
                        WriteLine($"{formatter.GetType().Name}");
                    }
                    else {
                        WriteLine(" {0}, Media types: {1}",
                        arg0:mediaFormatter.GetType().Name,
                        arg1:string.Join(", ", mediaFormatter.SupportedMediaTypes));
                    }
                }
            }
            ).AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddSwaggerGen(options=>{options.SwaggerDoc(name: "v1", info:new OpenApiInfo{Title = "Northwind Service App", Version="v1"});
            });

            services.AddHealthChecks().AddDbContextCheck<Northwind>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthwindService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");

                c.SupportedSubmitMethods(new[] {SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete});
            });

            app.UseHealthChecks(path: "/howdoyoufeel");

            app.Use(next => (content) => {
                var endpoint = content.GetEndpoint();
                if (endpoint != null) {
                    WriteLine("*** Name: {0}; Route: {1}; Metadata: {2}",
                    arg0: endpoint.DisplayName,
                    arg1: (endpoint as RouteEndpoint)?.RoutePattern,
                    arg2: string.Join(", ", endpoint.Metadata));
                }

                return next(content);
            });

            app.UseMiddleware<SecurityHeaders>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(configurePolicy: options=> {
                options.WithMethods("GET", "POST", "PUT", "DELETE");    
                options.WithMethods("https://localhost:5002");
            });
        }
    }
}
