using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Authorization;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{

    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, OpRole.AdminRole);

                // allowed user can create and edit contacts that they create
                var supervisorID = await EnsureUser(serviceProvider, testUserPw, "client@contoso.com");
                //await EnsureRole(serviceProvider, supervisorID, Constants.SupervisorRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Product.Any())
            {
                return;   // DB has been seeded
            }

            context.Category.AddRange(
                new Category { CategoryName = "Women" },
                new Category { CategoryName = "Men" },
                new Category { CategoryName = "Children" }
                );

            context.Product.AddRange(
                new Product
                {
                    ProductName = "Debra Garcia",
                    Category = "Women",
                    Description = "desc",
                    Price = 9.99M,
                },
                new Product
                {
                    ProductName = "Thorsten Weinrich",
                    Category = "Men",
                    Description = "desc",
                    Price = 9.99M,
                },
                new Product
                {
                    ProductName = "Yuhong Li",
                    Category = "Children",
                    Description = "desc",
                    Price = 9.99M,
                },
                new Product
                {
                    ProductName = "Jon Orton",
                    Category = "Women",
                    Description = "desc",
                    Price = 9.99M,
                },
                new Product
                {
                    ProductName = "Diliana Alexieva-Bosseva",
                    Category = "men",
                    Description = "desc",
                    Price = 9.99M,
                }
             );
            context.SaveChanges();
        }
    }
}


