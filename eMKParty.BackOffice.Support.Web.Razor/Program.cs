using eMKParty.BackOffice.Support.Infrastructure.Persistence.Contexts;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using eMKParty.BackOffice.Support.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using eMKParty.BackOffice.Support.Infrastructure.Extensions;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.ExpireTimeSpan = TimeSpan.FromHours(10);
           options.LoginPath = "/Account/Login/";
           options.LogoutPath = "/Account/Logout/";
           options.AccessDeniedPath = "/Account/AccessDenied";
       });

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// Add services to the container.
builder.Services.AddRazorPages();

//change 15/05/2024
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});
//end change 15/05/2024

var configValue = builder.Configuration.GetValue<string>("DefaultValues:SiteDefinition");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

//change 15/05/2024
app.MapControllers();
//app.UsePathBase("/web_test");

//app.Use((context, next) =>
//{
//    context.Request.PathBase = "/web_test";
//    return next();
//});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();

