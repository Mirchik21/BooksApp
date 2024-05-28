using Application;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using System.Net;
using Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddNotyf(config => { config.DurationInSeconds = 300; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });

builder.Services.AddWebConfiguration().AddApplication()
                .AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.WebHost
   .ConfigureKestrel(options => options.Listen(IPAddress.Loopback, 5050));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LogoutPath = "/account/login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseNotyf();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}");
});

app.Run();