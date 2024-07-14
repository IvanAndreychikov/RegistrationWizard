using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL.Context;
using RegistrationWizard.Helpers;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration["ConnectionString"] ?? 
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddRepositories();
builder.Services.AddServices();

var defaultCorsPolicy = "Default";
var allowedHosts = builder.Configuration["AllowedHosts"] ?? builder.Configuration["DefaultAllowedHosts"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: defaultCorsPolicy,
                      policy =>
                      {
                          policy.WithOrigins(allowedHosts)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseCors(defaultCorsPolicy);

app.Run();