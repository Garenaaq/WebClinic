using Microsoft.AspNetCore.Identity;
using WebClinic.Models.DomainModels;

namespace WebClinic.Data
{
    public class Initializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ConfigurationManager configuration)
        {
            string adminUsername = configuration["adminUsername"] ?? "admin";
            string adminPassword = configuration["adminPassword"] ?? "Admin1!";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }

            if (await roleManager.FindByNameAsync("doctor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("doctor"));
            }

            if (await roleManager.FindByNameAsync("patient") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("patient"));
            }

            if (await userManager.FindByNameAsync(adminUsername) == null)
            {
                User admin = new User() { UserName = adminUsername };

                IdentityResult createAction = await userManager.CreateAsync(admin, adminPassword);

                if (createAction.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }



        }
    }
}
