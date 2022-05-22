using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseStaticFiles();
app.UseMiddleware<CultureCookieHandlerMiddlewares>();
app.MapRazorPages();
app.Run();
