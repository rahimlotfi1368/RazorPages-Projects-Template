global using Infrastructure.Middlewares;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Utilities;
global using Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

//builder.Services.Configure<RequestLocalizationOptions>(option =>
//{
//    var SupportedCultures = new[]
//    {
//        new System.Globalization.CultureInfo("fa-IR"),
//        new System.Globalization.CultureInfo("en-US"),
//    };

//    option.SupportedCultures = SupportedCultures;
//    option.SupportedUICultures = SupportedCultures;

//    option.DefaultRequestCulture=new Microsoft.AspNetCore.Localization.RequestCulture(culture: "fa-IR",uiCulture:"fa-IR");
//});

builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>(builder.Configuration.GetSection(key: ApplicationSettings.KeyName));

var app = builder.Build();

app.UseStaticFiles();

app.UseCultureCookie();

app.MapRazorPages();

app.Run();
