using Microsoft.AspNetCore.Authentication.Cookies;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DAL.Interfaces;
using AutoMapper;
using SuppManagerDB.WebApp.App.MappingProfiles;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// DAL + BL реєстрація сервісів
// AUTH 
builder.Services.AddTransient<IUserDal, UserDal>();
builder.Services.AddTransient<IUserPrivilegeDal, UserPrivilegeDal>();
builder.Services.AddTransient<IAuthManager, AuthManager>();
// SUPPLIER 
builder.Services.AddTransient<ISupplierDal, SupplierDal>();
builder.Services.AddTransient<ISupplierManager, SupplierManager>();

builder.Services.AddSingleton<IMapper>(sp =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<SupplierProfile>();
    }, loggerFactory);

    return config.CreateMapper();
});

// COOKIE AUTH налаштування аутентифікації 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/Forbidden";
        options.LoginPath = "/Account/Login";
    });



//////
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
