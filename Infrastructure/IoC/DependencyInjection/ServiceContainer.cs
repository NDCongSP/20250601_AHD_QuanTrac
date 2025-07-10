using Application.Services.Authen.UI;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Infrastructure.IoC.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging(false)
        );
        services.AddIdentity<ApplicationUser, IdentityRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.Password.RequireNonAlphanumeric = true;
            o.Password.RequiredLength = 8;
            o.Password.RequiredUniqueChars = 0;
            o.Lockout.MaxFailedAccessAttempts = 5;
            o.SignIn.RequireConfirmedAccount = false;
            o.SignIn.RequireConfirmedEmail = false;
            o.SignIn.RequireConfirmedPhoneNumber = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();


        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            var keyr = config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,                    
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = securityKey,
                
            };

            option.Events = new JwtBearerEvents()
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    // Ensure we always have an error and error description.
                    if (string.IsNullOrEmpty(context.Error))
                        context.Error = "invalid_token";

                    if (string.IsNullOrEmpty(context.ErrorDescription))
                        context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                    // Add some extra context for expired tokens.
                    if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                        context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                        context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
                    }

                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        error = context.Error,
                        error_description = context.ErrorDescription
                    }));
                }
            };

        });
        services.AddAuthentication();
        services.AddAuthorization();

        //Read config cors
        var corsSettings = config.GetSection("Cors");
        var anyClient = corsSettings.GetSection("AnyClient").Get<bool>();
        var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();

        if (anyClient)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("UI",
                    builder => builder                                    
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                    );
            });
        }
        else
        {
            services.AddCors(option =>
            {
                option.AddPolicy("UI",
                    builder => builder
                                .WithOrigins(allowedOrigins)                                    
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                    );
            });
        }
        ServiceAddScoped.RegisterServices(services);
        return services;
    }
}
