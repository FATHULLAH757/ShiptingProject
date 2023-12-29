using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MainWebProject.Config
{

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Driver = "Driver";
    }
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider applicationBuilder)
        {
            try
            {
                //var scopeFactory = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                using (var serviceScope = applicationBuilder.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {

                    //Roles
                  //  var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
                        if (!await roleManager.RoleExistsAsync(UserRoles.Driver))
                             await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Driver));

                            //Users
                            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    string adminUserEmail = "admin@shippingproject.com";

                    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                    if (adminUser == null)
                    {
                        var newAdminUser = new User()
                        {
                            FullName = "Admin User",
                            UserName = "admin-user",
                            Email = adminUserEmail,
                            EmailConfirmed = true,
                            Description = "abc"
                        };
                        await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    }


                    string appUserEmail = "driver@shippingproject.com";

                    var appUser = await userManager.FindByEmailAsync(appUserEmail);
                    if (appUser == null)
                    {
                        var newAppUser = new User()
                        {
                            FullName = "Application User",
                            UserName = "driver-user",
                            Email = appUserEmail,
                            EmailConfirmed = true,
                            Description = "abc"
                        };
                        await userManager.CreateAsync(newAppUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.Driver);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
