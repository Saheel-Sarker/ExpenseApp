using ExpenseApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var rawUrl = Environment.GetEnvironmentVariable("DATABASE_URL");


if (string.IsNullOrEmpty(rawUrl))
    throw new Exception("DATABASE_URL environment variable not found!");

// Convert URI to key-value format
var databaseUri = new Uri(rawUrl);
var userInfo = databaseUri.UserInfo.Split(':');

var port = databaseUri.Port > 0 ? databaseUri.Port : 5432;

var npgsqlConnStr =
    $"Host={databaseUri.Host};" +
    $"Port={port};" +
    $"Username={userInfo[0]};" +
    $"Password={userInfo[1]};" +
    $"Database={databaseUri.AbsolutePath.TrimStart('/')};" +
    $"SSL Mode=Require;Trust Server Certificate=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(npgsqlConnStr));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
