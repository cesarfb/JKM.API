using JKM.PERSISTENCE.GlobalErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace JKM.API
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
            services.AddControllers();

            //DOCUMENTATION
            services.AddSwaggerGen(c =>
            {
                //ANNOTATIONS
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JKM API",
                    Description = "API JKM PUBLICA",
                });
            });

            //CORES
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            //INIT DATABASE CONN
            string cnn = Configuration.GetValue<string>("ConnectionStrings:DB_JKM");
            services.AddScoped<IDbConnection>(x => new SqlConnection(cnn));

            //INIT SMTP EMAILS
            services.AddTransient<SmtpClient>((serviceProvider) =>
            {
                IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<String>("Smtp:Host"),
                    Port = config.GetValue<int>("Smtp:Port"),
                    Credentials = new NetworkCredential(config.GetValue<String>("Smtp:Username"), config.GetValue<String>("Smtp:Password")),
                    EnableSsl = true,
                    
                };
            });

            services.AddTransient<MailMessage>((serviceProvider) =>
            {
                IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
                return new MailMessage()
                {
                    From = new MailAddress(config.GetValue<string>("Smtp:From"), config.GetValue<string>("Smtp:DisplayName")),
                    IsBodyHtml = true
                };
            });

            //MEDIATR
            Assembly assembly = AppDomain.CurrentDomain.Load("JKM.APPLICATION");
            services.AddMediatR(assembly);

            //INYECCIONES DE REPOSITORY
            Assembly interfaces = AppDomain.CurrentDomain.Load("JKM.PERSISTENCE");
            List<Type> repositories = interfaces.GetTypes()
                .Where(g => g.Name.IndexOf("Repo") >= 0)
                .ToList();

            for (int i = 0; i < repositories.Count; i += 2)
            {
                Type repo1 = repositories[i], repo2 = repositories[i + 1];
                bool interfazBool = repositories[i].Attributes.HasFlag(TypeAttributes.Abstract);
                if (interfazBool)
                    services.AddTransient(repo1, repo2);
                else
                    services.AddTransient(repo2, repo1);
            }

            //AGREGAMOS LA LIBRERIA PARA PODER PARSEAR LOS ERRORES EN EL BADREQUEST @3.1.2
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //HANDLE EXCEPTIONS
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //SWAGGER
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
                /////////
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //CORES
            app.UseCors("AllowAll");
            ///////

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
