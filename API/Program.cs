using Infrastructure.Data;
using Infrastructure.IoC.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var env = config["Environment"];
if (!String.IsNullOrEmpty(env))
{
    config.AddJsonFile(string.Format("appsettings.{0}.json", env), optional: false, reloadOnChange: true);
}
var con = config.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.CustomOperationIds(desc =>
    {
        if (desc.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            return $"{descriptor.ControllerName}_{descriptor.ActionName}";
        }
        return desc.GroupName;
    });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AHD API",
        Version = "v1",
        Description = "This is API description.",
        Contact = new OpenApiContact
        {
            Name = "AHD Information",
            Email = "your.email@example.com",
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "Input your Bearer token to access this API",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "Bearer",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
});

builder.Services.AddInfrastructureService(config);

var app = builder.Build();

// Áp dụng CORS trước UseAuthorization
app.UseCors("WebUI");

// Seeding the database when the application starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Call the seeding method
    await DbInitializer.InitializeAsync(context);
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AHD API V1");
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
