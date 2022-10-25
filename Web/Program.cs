using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net;
using Web.Settings;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add files to configuration
builder.Configuration.AddJsonFile("appsettings.json", false, true);

// Add services
builder.Services.AddRouting();
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton(x =>
	ActivatorUtilities.CreateInstance<MongoClient>(x, x.GetRequiredService<IOptions<MongoDBSettings>>().Value.ConnectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					 .AddCookie(opts =>
					 {
						 opts.LoginPath = new PathString("/account/login");
					 });

builder.Services.AddAuthorization(opts =>
{
	opts.AddPolicy(AuthPolicies.AllUsersPolicy.Name, AuthPolicies.AllUsersPolicy.Action);
	opts.AddPolicy(AuthPolicies.AdminOnlyPolicy.Name, AuthPolicies.AdminOnlyPolicy.Action);
});

// Add autofac container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule<CoreAutofacModule>());

// Build app
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Run
app.Run();