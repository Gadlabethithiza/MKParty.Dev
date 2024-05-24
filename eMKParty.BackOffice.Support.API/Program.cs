using eMKParty.BackOffice.Support.Application.Extensions;
using eMKParty.BackOffice.Support.Infrastructure.Extensions;
using eMKParty.BackOffice.Support.Infrastructure.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);

    //var logger = new LoggerConfiguration()
    //    .ReadFrom.Configuration(builder.Configuration)
    //    .Enrich.FromLogContext()
    //    .CreateLogger();

    //builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    //Add support to logging with SERILOG
    //builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

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

    //change 15/05/2024
    builder.Services.AddMvc(options =>
    {
        options.EnableEndpointRouting = false;
    });
    //end change 15/05/2024


    var contact = new OpenApiContact()
    {
        Name = "Bheki Mkhize",
        Email = "bheki@gcwabetech.co.za",
        Url = new Uri("http://www.gcwabetech.co.za")
    };

    var license = new OpenApiLicense()
    {
        Name = "My Licensing Conditions",
        Url = new Uri("http://www.gcwabetech.co.za")
    };

    var information = new OpenApiInfo()
    {
        Version = "v1",
        Title = "uMkhonto Wesizwe Secured Basic API",
        Description = "Welcome to uMkhonto weSizwe Party, a powerful force dedicated to bringing about positive change and progress in society. As a true liberation movement, we stand for the people, by the people, and with the people.",
        TermsOfService = new Uri("http://www.gcwabetech.co.za"),
        Contact = contact,
        License = license
    };

    builder.Services.AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", information);
    });

    var configValue = builder.Configuration.GetValue<string>("DefaultValues:SiteDefinition");

    var app = builder.Build();

    //change 15/05/2024
    app.MapControllers();
    app.UsePathBase("/api");

    app.Use((context, next) =>
    {
        context.Request.PathBase = "/api";
        return next();
    });

    app.UseMvc();
    //end change 15/05/2024

    app.UseAuthentication(); //Student have valid ID
    app.UseAuthorization(); // IS the student above 18 ?

    app.UseSwagger();
    app.UseStaticFiles();

    //Add support to logging request with SERILOG
    app.UseSerilogRequestLogging();

    app.UseSwaggerUI(c =>
    {
        if (!string.IsNullOrEmpty(configValue))
        {
            c.SwaggerEndpoint("/" + configValue + "/swagger/v1/swagger.json", "uMkhonto Wesizwe Secured Basic API");
            c.InjectStylesheet("/" + configValue + "/swagger-ui/custom.css");
            c.InjectJavascript("/" + configValue + "/swagger-ui/custom.js");
        }
        else
        {
            //c.SwaggerEndpoint("v1/swagger.json", "uMkhonto Wesizwe Secured Basic API");
            //c.InjectStylesheet("/custom.css");
            //c.InjectJavascript("/custom.js");

            c.SwaggerEndpoint("/swagger/v1/swagger.json", "uMkhonto Wesizwe Secured Basic API");
            c.InjectStylesheet("/swagger-ui/custom.css");
            c.InjectJavascript("/swagger-ui/custom.js");
        }

        c.DocumentTitle = "uMkhonto Wesizwe Secured Basic API";
        c.DefaultModelExpandDepth(0);
        c.DefaultModelsExpandDepth(-1);
    });


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}