using Calorie_Tracking_App.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["Authentication:AUTHORITY"];
    options.ClientId = builder.Configuration["Authentication:CLIENTID"];
    options.ClientSecret = builder.Configuration["Authentication:CLIENTSECRET"];

    options.ResponseType = "code"; // recommended for OIDC
    options.SaveTokens = true;

    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CalorieTrackingContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
