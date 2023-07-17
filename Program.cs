using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var path = context.Request.Path;
    var now = DateTime.Now;
    var response = context.Response;
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    response.Headers.ContentType = "text/plain; charset=utf-8";

    if (string.IsNullOrEmpty(path))
    {
        await context.Response.WriteAsync("Привіт користувач NET 6.0");
    }
    else if (path == "/info")
    {
        await context.Response.WriteAsync("Host: " + context.Request.Host.Value + "\n");
        await context.Response.WriteAsync("Path: " + context.Request.Path + "\n");
        await context.Response.WriteAsync("QueryString: " + context.Request.QueryString);
    }
    else if (path == "/time")
    {
        await context.Response.WriteAsync(DateTime.Now.ToString());
    }
    else if (path == "/key")
    {
        var key = configuration["Key"];
        await context.Response.WriteAsync(key);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not found");
    }
});

app.Run();
