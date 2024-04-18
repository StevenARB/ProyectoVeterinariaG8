using Microsoft.EntityFrameworkCore;
using ProyectoVeterinariaG8.DAL;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionStringVeterinaria = builder.Configuration.GetConnectionString("VeterinariaContextConnection") ?? throw new InvalidOperationException("Connection string 'VeterinariaContextConnection' not found.");
var connectionStringAuth = builder.Configuration.GetConnectionString("AuthContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<VeterinariaContext>(options => options
    .UseSqlServer(connectionStringVeterinaria)
    .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddDbContext<AuthContext>(options => options
    .UseSqlServer(connectionStringAuth)
    .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthContext>()
    .AddDefaultUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
