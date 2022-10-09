using Autofac;
using HTTPAPI;
using Core.Configuration;
using Autofac.Extensions.DependencyInjection;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json");

// IoC configuration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule<CoreAutofacModule>());

// Services
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRouting();

builder.Services.AddDbContext<MainDbContext>(options => 
   options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["db"]));

// Application
var app = builder.Build();

// Routes
app.UseRouting();
app.MapControllers();

// Run
app.Run();
