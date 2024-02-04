using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebClinic.Data;
using WebClinic.Models.DomainModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration["Database"] ?? throw new Exception("Database doesn't exist");

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ClinicContext>(options => options.UseNpgsql(builder.Configuration["Database"]));
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<ClinicContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

using (var init = app.Services.CreateScope())
{
    try
    {
        UserManager<User>? userManager = init.ServiceProvider.GetService<UserManager<User>>();
        RoleManager<IdentityRole<int>>? roleManager = init.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();
        await Initializer.InitializeAsync(userManager, roleManager, builder.Configuration);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

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

app.MapRazorPages();

app.Run();