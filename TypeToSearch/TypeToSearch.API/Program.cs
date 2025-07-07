using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TypeToSearch.API.Middlewares;
using TypeToSearch.Application;
using TypeToSearch.Infrastructure;

namespace TypeToSearch.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            #region Swagger Options

            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TypeToSearch.API",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira 'Bearer' [espaco] e o token JWT valido na caixa de texto abaixo.\nExemplo: 'Bearer eyJhbGciOiJIU'",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            #endregion

            #region Jwt Authentication

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:secretkey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            #endregion

            #region Dependencies
            ServiceInfrastructureExtensions.AddInfrastructureServices(builder.Services);
            ServiceApplicationExtensions.AddApplicationServices(builder.Services);
            #endregion

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TypeToSearch.API v1");
            });

            // Permissões (de qualquer origem, metodo Http e cabeçalho)
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            #region Middlewares
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthorization();
            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
