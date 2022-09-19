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
                    ProductName = "Debrah Garcias dress-W",
                    Category = "Women",
                    Description = "Black sleevless Mid-dress",
                    Price = 299.99M,
                    ImgUrl = "Image/Women/dress1.jpg"
                },
                 new Product
                 {
                     ProductName = "Jon Ortons dress-W",
                     Category = "Women",
                     Description = "Camel long sleevless silk dress",
                     Price = 400.98M,
                     ImgUrl = "Image/Women/dress2.jpg"
                 },
                  new Product
                  {
                      ProductName = "Louis Vouitonx dress-W",
                      Category = "Women",
                      Description = "Colorfull long sleeve mid-dress",
                      Price = 450M,
                      ImgUrl = "Image/Women/dress3.jpg"
                  },
                   new Product
                   {
                       ProductName = "GucciV dress-W",
                       Category = "Women",
                       Description = "Dark green casual pants",
                       Price = 230M,
                       ImgUrl = "Image/Women/pants1.jpg"
                   },
                    new Product
                    {
                        ProductName = "PradaV pants-W",
                        Category = "Women",
                        Description = "Camel bulky cotton pants",
                        Price = 520.00M,
                        ImgUrl = "Image/Women/pants2.jpg"
                    },
                     new Product
                     {
                         ProductName = "GucciV Pants-W",
                         Category = "Women",
                         Description = "Blue jeans",
                         Price = 600M,
                         ImgUrl = "Image/Women/pants3.jpg"
                     },
                      new Product
                      {
                          ProductName = "GucciV top-W",
                          Category = "Women",
                          Description = "Blue silk long sleeve ",
                          Price = 440M,
                          ImgUrl = "Image/Women/top1.jpg"
                      },
                        new Product
                        {
                            ProductName = "BurberryX Shirt-W",
                            Category = "Women",
                            Description = "Blue down shoulder long sleeve shirt",
                            Price = 290M,
                            ImgUrl = "Image/Women/top2.jpg"
                        },
                          new Product
                          {
                              ProductName = "GuessX Shirt-W",
                              Category = "Women",
                              Description = "Gray half sleeve cotton top",
                              Price = 190M,
                              ImgUrl = "Image/Women/top3.jpg"
                          },
                            new Product
                            {
                                ProductName = "GucciV Top-W",
                                Category = "Women",
                                Description = "Dark green long sleeve zipper front",
                                Price = 520M,
                                ImgUrl = "Image/Women/top4.jpg"
                            },
                 new Product
                 {
                     ProductName = "Weinrich pants-M",
                     Category = "Men",
                     Description = "Light black formal silk pants",
                     Price = 320M,
                     ImgUrl = "Image/Men/menPants1.jpg"
                 },
                  new Product
                  {
                      ProductName = "GucciX pants-M",
                      Category = "Men",
                      Description = "Beige kaki pants",
                      Price = 440M,
                      ImgUrl = "Image/Men/menPants2.jpg"
                  },
                   new Product
                   {
                       ProductName = "ThorstenH pants-M",
                       Category = "Men",
                       Description = "Beige active pants",
                       Price = 120M,
                       ImgUrl = "Image/Men/menPants3.jpg"
                   },
                    new Product
                    {
                        ProductName = "ThorstenH shorts-M",
                        Category = "Men",
                        Description = "Dark green pocket short",
                        Price = 120M,
                        ImgUrl = "Image/Men/menShorts1.jpg"
                    },
                    new Product
                    {
                        ProductName = "Thorsten shorts-M",
                        Category = "Men",
                        Description = "Black active short",
                        Price = 97.99M,
                        ImgUrl = "Image/Men/menShorts2.jpg"
                    },
                    new Product
                    {
                        ProductName = "PradaV shorts-M",
                        Category = "Men",
                        Description = "Short blue jeans with pockets",
                        Price = 90.99M,
                        ImgUrl = "Image/Men/menShorts3.jpg"
                    },
                new Product
                {
                    ProductName = "PradaV top-M",
                    Category = "Men",
                    Description = "Blue black long sleeve cotton top",
                    Price = 389.90M,
                    ImgUrl = "Image/Men/menTops1.jpg"
                },
                 new Product
                 {
                     ProductName = "Thorsten Weinrich top-M",
                     Category = "Men",
                     Description = "Gray silk half sleeve pocket shirt",
                     Price = 190M,
                     ImgUrl = "Image/Men/menTops2.jpg"
                 },
                  new Product
                  {
                      ProductName = "BurberryX top-M",
                      Category = "Men",
                      Description = "Gray silk long sleeve pocket shirt",
                      Price = 210M,
                      ImgUrl = "Image/Men/menTops3.jpg"
                  },
                   new Product
                   {
                       ProductName = "Nike top-M",
                       Category = "Men",
                       Description = "Black half sleeve cotton top",
                       Price = 90M,
                       ImgUrl = "Image/Men/menTops4.jpg"
                   },

                new Product
                {
                    ProductName = "BalloonX dress-G",
                    Category = "Children",
                    Description = "GucciX balloon chic dress",
                    Price = 300M,
                    ImgUrl = "Image/Children/BalloonChicDress.jpg"

                },
                new Product
                {
                    ProductName = "D&G Swimwear sw-G",
                    Category = "Children",
                    Description = "D&G girls one piece Swimwear",
                    Price = 300M,
                    ImgUrl = "Image/Children/D&GSwimwear.jpg"

                },
                new Product
                {
                    ProductName = "FendiX dress-G",
                    Category = "Children",
                    Description = "FendiX girls chic dress",
                    Price = 460M,
                    ImgUrl = "Image/Children/dressFendi.jpg"

                },
                 new Product
                 {
                     ProductName = "GucciV dress-G",
                     Category = "Children",
                     Description = "GucciV girls grey chic dress",
                     Price = 760M,
                     ImgUrl = "Image/Children/dressGucci.jpg"

                 },
                  new Product
                  {
                      ProductName = "GucciV  top-B",
                      Category = "Children",
                      Description = "GucciV boys grey chic top",
                      Price = 510M,
                      ImgUrl = "Image/Children/GucciBoy.jpg"

                  },
                   new Product
                   {
                       ProductName = "Kenzo top-B",
                       Category = "Children",
                       Description = "White long sleeve sweater",
                       Price = 390M,
                       ImgUrl = "Image/Children/KenzoSweater.jpg"

                   },
                    new Product
                    {
                        ProductName = "Lacoste top-B",
                        Category = "Children",
                        Description = "White short sleeve top",
                        Price = 120M,
                        ImgUrl = "Image/Children/LacosteBoyShirt.jpg"

                    },
                     new Product
                     {
                         ProductName = "Lacoste sweater-B",
                         Category = "Children",
                         Description = "Blue long sleeve sweater",
                         Price = 220M,
                         ImgUrl = "Image/Children/LacosteBoySweater.jpg"

                     },
                      new Product
                      {
                          ProductName = "RalphPolo Skirt-G",
                          Category = "Children",
                          Description = "Pink cotton girls skirt",
                          Price = 180M,
                          ImgUrl = "Image/Children/RalphPoloSkirt.jpg"

                      },
                      new Product
                      {
                          ProductName = "VersaceV Swimwear Skirt-G",
                          Category = "Children",
                          Description = "Pink girls swimwear",
                          Price = 680M,
                          ImgUrl = "Image/Children/VersaceSwimwear.jpg"

                      }

             );
            context.SaveChanges();
        }
    }
}


