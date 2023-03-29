using SuperSold.UI.AspDotNet.Constants;
using SuperSold.UI.AspDotNet.HostBuilders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySql") ?? throw new Exception("The connection string 'MySql' has not been provided in appsettings.json");

// Add services to the container.
//builder.Services.AddMemoryDatabase();
builder.Services.AddMySqlDatabase(connectionString);
builder.Services.AddAuthenticationHelpers();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(Cookies.Auth).AddCookie(Cookies.Auth, options => {
    options.Cookie.Name = Cookies.Auth;
});

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
