using MelonBookchelfApi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace MelonBookshelfApi.ApplicationExtentions
{
    public static class Initializer
    {
        public static void Initialize(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<BookshelfDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            var roleCheck = roleManager.RoleExistsAsync("Admin").Result;
            if (!roleCheck)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
            }

            IdentityUser user = userManager.FindByEmailAsync("davidpetkov2006@gmail.com").Result;
            var userRoleCheck = userManager.IsInRoleAsync(user, "Admin").Result;
            if (!userRoleCheck)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
