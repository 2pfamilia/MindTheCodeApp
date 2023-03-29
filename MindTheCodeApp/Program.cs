using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using AppCore.IRepositories;
using AppCore.Services.Implementation;
using MindTheCodeApp.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using MindTheCodeApp.Utils;
using MindTheCodeApp.Services.Implementation;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Database.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});

// Authentication service with Cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name = "CookieAuth";
    options.LoginPath = "/";
    options.LogoutPath = "/my-account/Logout";
    options.AccessDeniedPath = "/";
});

// Database population service that runs when the database is empty.
builder.Services.AddHostedService<PopulateDb>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Utility services
builder.Services.AddScoped<CsvUtils>();

//Add Repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Add Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[]
{
    new CultureInfo("en-GB"),
    new CultureInfo("el")
};

builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture("en-GB");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services
    .AddControllersWithViews()
    .AddViewLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
