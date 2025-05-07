using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Shaghaf.Infrastructure.IdentityData.SeedData
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName)
                    {
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    roleResult = await roleManager.CreateAsync(role);
                }
            }

            // Update existing roles with NULL ConcurrencyStamp
            var rolesWithNullStamp = context.Roles.Where(r => r.ConcurrencyStamp == null);
            foreach (var role in rolesWithNullStamp)
            {
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
            }
            await context.SaveChangesAsync();
        }
    }

}
