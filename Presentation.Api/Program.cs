using MassTransit;
using Application.Consumers;
using Services.Contracts;
using Data.Integrations;
using Data.Repositories;
using Services.Impl;

var builder = WebApplication.CreateBuilder(args);
// ------------------------------------------------------------
// Load configuration
// ------------------------------------------------------------
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();
var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);
// ------------------------------------------------------------
// Kestrel (Run Server)
// ------------------------------------------------------------
Console.WriteLine("Loading Kestrel ...");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    try
    {
        serverOptions.ListenAnyIP(8090);
        serverOptions.ListenAnyIP(8091, listenOptions =>
        {
            listenOptions.UseHttps(); // Listen for HTTPS traffic on port 8999
        });
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error configuring HTTPS: {e.Message}");
    }
    Console.WriteLine("Kestrel listening on 8090 (http) and 8091 (https)");
});

// ------------------------------------------------------------
// Core ASP.NET services
// ------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// ------------------------------------------------------------
// Wiring MassTransit
// ------------------------------------------------------------
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<GetCustomerSummaryConsumer>();
    cfg.AddConsumer<SendNotificationConsumer>();
    cfg.AddConsumer<CreateOrderConsumer>();
});
// ------------------------------------------------------------
// Custom Dependency Injection (EXTENSIONS)
// ------------------------------------------------------------
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerStatusClient, CustomerStatusClient>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IEmailClient, SesEmailClient>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
// ------------------------------------------------------------
// Build application
// ------------------------------------------------------------
var app = builder.Build();
// ------------------------------------------------------------
// Middleware
// ------------------------------------------------------------
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine("Running Application...");
app.Run();