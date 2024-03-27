using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novella.Data;
using Novella.Data.Services;
using Novella.EfModels;
using Novella.Services;
using Novella.Models;
using Novella.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register the DbContexts with SQL Server
builder.Services.AddDbContext<NovellaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Set up Identity with roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Additional services
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IEmailService, EmailService>();
// Register a null IEmailSender service
builder.Services.AddTransient<IEmailSender, NullEmailSender>();

// Register the authorization services
builder.Services.AddAuthorization();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register MVC services for controllers and views
builder.Services.AddControllersWithViews(); // For MVC applications

builder.Services.AddSession(options =>
{
    // Set session options here (optional)
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Example: set session timeout to 30 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Razor Pages services
builder.Services.AddRazorPages();

// Register ProductRepo as a scoped service
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<OtherUserRepo>();
builder.Services.AddScoped<OrderRepo>();
builder.Services.AddScoped<AddressRepo>();
builder.Services.AddScoped<ProductOrderRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Setup authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession(); // Add this before UseMvc, UseEndpoints, or similar middleware


// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages
app.MapRazorPages();

app.Run();
