using AzureClassLibrary.Monitor;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add application insights.

// 📖 Enable the Application Insight Service in your application
// 1. If you configured the connection string in the appsettings.json,
// you can use a single line code to enable the Application Insights service in your program:
// builder.Services.AddApplicationInsightsTelemetry();
// 2. If you want config by environment please use the following code:
var appInsConnStr = Environment.GetEnvironmentVariable("APPLICATION_INSIGHTS_CONNECTION_STRING") ?? ""; 
var appInsOptions = new ApplicationInsightsServiceOptions { ConnectionString = appInsConnStr };
builder.Services.AddApplicationInsightsTelemetry(options:appInsOptions);

// 📖 Config the service/module name which will show in the application map. 
builder.Services.AddSingleton<ITelemetryInitializer>(new CustomTelemetryInitializer("MVC2"));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();