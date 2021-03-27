using AutoMapper;
using ContactKeeperApi.Application.Filters;
using ContactKeeperApi.Application.Infrastructure;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.Services;
using ContactKeeperApi.Application.User.Commands.Create;
using ContactKeeperApi.Common;
using ContactKeeperApi.Infra.Configurations;
using ContactKeeperApi.Infra.Context;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace ContactKeeperAPI
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
            //Adicionando compressão
            services.AddResponseCompression(option =>
            {
                option.Providers.Add<GzipCompressionProvider>();
                option.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            //Adiciona cache na aplicação inteira
            //Coloquei no método de autenticação um exemplo para usar em uma rota específica
            // services.AddResponseCaching();

            var settings = Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();

            var key = Encoding.ASCII.GetBytes(settings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(x => x.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build()));


            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddControllers();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddOpenApiDocument(x =>
            {
                x.Title = "ContactKeeper";
                x.Description = "Api criada para estudo";
            });

            services.AddCors();

            //Configura��es do banco

            services.AddDbContext<IContactKeeperContext, ContactKeeperContext>(options =>
           options.UseNpgsql(Configuration.GetConnectionString("Database")));

            //Adicionando MediatR
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

            //Adicionando FLuent Validator
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            })
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

            //Adicionando AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfiles).GetTypeInfo().Assembly });

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyHeader();
                option.AllowAnyMethod();
            });
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseReDoc(x =>
            {
                x.Path = "/redoc";
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
