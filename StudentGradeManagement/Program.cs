
using StudentGradeManagement.Data;   
using Microsoft.EntityFrameworkCore;
using StudentGradeManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies; 
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";       
        options.LogoutPath = "/Account/Logout";     
        options.AccessDeniedPath = "/Account/AccessDenied"; 
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Students}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    var admin = context.Users.FirstOrDefault(u => u.Username == "admin");
    if (admin == null)
    {
        context.Users.Add(new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), 
            Role = "Admin"
        });
        context.SaveChanges();
    }
    else if (!admin.PasswordHash.StartsWith("$2")) 
    {
        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");
        context.SaveChanges();
    }
}

app.Run();
