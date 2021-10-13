using FluentValidation;
using FluentValidation.AspNetCore;
using JKM.UTILITY.GlobalErrorHandling;
using JKM.UTILITY.Jwt;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

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
            Assembly application = AppDomain.CurrentDomain.Load("JKM.APPLICATION");
            Assembly persistence = AppDomain.CurrentDomain.Load("JKM.PERSISTENCE");

            //MIDDLEWARE FLUENT VALIDATION
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.ImplicitlyValidateRootCollectionElements = true;
                    fv.DisableDataAnnotationsValidation = true;

                    fv.RegisterValidatorsFromAssembly(application);
                })
                //AGREGAMOS LA LIBRERIA PARA PODER PARSEAR LOS ERRORES EN EL BADREQUEST @3.1.2
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            //ADD JWT
            services.Configure<TokenManager>(Configuration.GetSection("JwtConfig"));
            TokenManager token = Configuration.GetSection("JwtConfig").Get<TokenManager>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret))
                };
            });

            services.AddScoped<IJwtService, JwtService>();

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

                //BEARER
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {
                   new OpenApiSecurityScheme
                   {
                     Reference = new OpenApiReference
                     {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                     }
                    },
                    new string[] { }
                  }
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
            string cnn = Configuration.GetSection("ConnectionStrings").GetValue<string>("DB_JKM");
            services.AddScoped<IDbConnection>(x => new SqlConnection(cnn));

            //INIT SMTP EMAILS
            services.Configure<SmtpModel>(Configuration.GetSection("Smtp"));
            SmtpModel smtpModel = Configuration.GetSection("Smtp").Get<SmtpModel>();

            services.AddTransient<SmtpClient>((serviceProvider) => new SmtpClient()
            {
                Host = smtpModel.Host,
                Port = smtpModel.Port,
                Credentials = new NetworkCredential(smtpModel.Username, smtpModel.Password),
                EnableSsl = true,

            }
            ).AddTransient((serviceProvider) => new MailMessage()
            {
                From = new MailAddress(smtpModel.From, smtpModel.DisplayName),
                IsBodyHtml = true
            }
            );

            //MEDIATR
            services.AddMediatR(application);

            //INYECCIONES DE REPOSITORY
            List<Type> repositories = persistence.GetTypes()
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

            //ENABLE FOR TOKEN
            app.UseAuthentication();
            app.UseAuthorization();
            //////////////////
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
