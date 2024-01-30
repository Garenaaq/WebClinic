using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration["Database"] ?? throw new Exception("Database doesn't exist");

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ClinicContext>(options => options.UseNpgsql(builder.Configuration["Database"]));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<ClinicContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();