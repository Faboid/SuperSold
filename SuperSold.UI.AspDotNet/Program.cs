using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.HostBuilders;
using SuperSold.UI.AspDotNet.HostedServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySql") ?? throw new Exception("The connection string 'MySql' has not been provided in appsettings.json");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
//builder.Services.AddMemoryDatabase();
builder.Services.AddMySqlDatabase(connectionString);
builder.Services.AddStaticHtmlResources();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationHelpers();
builder.Services.AddEmailSupport();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(Cookies.Auth).AddCookie(Cookies.Auth, options => {
    options.Cookie.Name = Cookies.Auth;
});

builder.Services.AddHostedService(s => {
    return new ExpiredRollbacksCleanerHostedService(s, TimeSpan.FromMinutes(30));
});

builder.Services.AddAutoMapper();

builder.Services.AddAntiforgery();
var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
