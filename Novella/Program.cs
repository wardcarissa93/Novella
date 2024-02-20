using Novella.EfModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Novella.Data;
using Novella.Data.Services;
using Novella.Services;


var builder = WebApplication.CreateBuilder(args);

// Example SQLite connection string format
var connectionString = "Data Source=.\\wwwroot\\Novella.db;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)); // Use SQLite

builder.Services.AddDbContext<NovellaContext>(options =>
    options.UseSqlite(connectionString)); // Use SQLite

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); // Use SQL Server

builder.Services.AddDbContext<NovellaContext>(options =>
    options.UseSqlServer(connectionString)); // Use SQL Server


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();




builder.Services.AddTransient<IEmailService, EmailService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
