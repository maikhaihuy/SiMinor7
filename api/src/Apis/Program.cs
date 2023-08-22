using System;
using Apis.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiMinor7.Application.Common.Settings;
using SiMinor7.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;
const string corsName = "HmCors";
var policyName = $"{corsName}_{environmentName}";

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApisServices();

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod();

        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin();
        }
        else
        {
            var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            policy.WithOrigins(origins);
        }
    });
});

var emailSetting = builder.Configuration.GetRequiredSection("MailSettings").Get<MailSettings>()!;

builder.Services.AddFluentEmail(emailSetting.UserName, emailSetting.DisplayName)
        .AddLiquidRenderer(options =>
        {
            // file provider is used to resolve layout files if they are in use
            //options.FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Layout"));
        })
        .AddSmtpSender(emailSetting.Host, emailSetting.Port, emailSetting.UserName, emailSetting.Password);

builder.Services.AddDependenciesInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi3();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<SiMinor7DbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(policyName);

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();