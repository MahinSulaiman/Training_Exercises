using ClaysysLearningPortal.DAL;
using ClaysysLearningPortal.Error;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CoursesDAL>();
builder.Services.AddScoped<UserDAL>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ErrorLogger>();
// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
    });
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();  
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
