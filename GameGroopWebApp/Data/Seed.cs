using GameGroopWebApp.Data.Enum;
using GameGroopWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace GameGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Game Club MOBA",
                            Image = "https://img.freepik.com/free-photo/male-hands-hold-gamepad-blue-background-copy-space_169016-17528.jpg?w=1380&t=st=1663759201~exp=1663759801~hmac=5eb27bf6b6227897b156c8c7c083a30f933bccae51badf7d4f059cf7cd848dbc",
                            Description = "This is the description of the MOBA club",
                            ClubCategory = ClubCategory.MOBA,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                         },
                        new Club()
                        {
                            Title = "Game Club Shooter",
                            Image = "https://img.freepik.com/free-vector/release-hostages-operation-concept_1284-37786.jpg?w=1060&t=st=1663765273~exp=1663765873~hmac=9511f44a2b27b96b293ef801ce5e65a2b49b1e3148432eb3867c7d3d7080fc68",
                            Description = "This is the description of the Shooter club",
                            ClubCategory = ClubCategory.Shooter,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Game Club Sandbox",
                            Image = "https://img.freepik.com/free-photo/children-toys-sandbox-colorful-surface_23-2148149534.jpg?w=1060&t=st=1663765321~exp=1663765921~hmac=f2929afdd11c0654f5cbedb5b2ca1b5a3a9732ad7cfa34398f40b3560608fc90",
                            Description = "This is the description of the Sandbox club",
                            ClubCategory = ClubCategory.Sandbox,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Game Club Puzzlers",
                            Image = "https://img.freepik.com/free-photo/close-up-puzzle-background_23-2149289238.jpg?w=1060&t=st=1663765386~exp=1663765986~hmac=2a8e98a3351050c2c0e33d434ddb0e80fab1c27f71961ade2dcec9c1ff8dbeec",
                            Description = "This is the description of the Puzzlers club",
                            ClubCategory = ClubCategory.Puzzlers,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Events
                if (!context.Events.Any())
                {
                    context.Events.AddRange(new List<Events>()
                    {
                        new Events()
                        {
                            Title = "Game Event GamePlaing",
                            Image = "https://img.freepik.com/free-photo/group-frustrated-friends-losing-video-games-play-with-vr-glasses-console-drinking-bottles-beer-friends-feeling-sad-about-lost-gameplay-competition-having-fun-together-gathering_482257-49239.jpg?w=1060&t=st=1663766623~exp=1663767223~hmac=08726b8765cafd09f3bf853bcc82ec5539683eb01a62af841956a349300ab154",
                            Description = "This is the description of the GamePlaing",
                            EventsCategory = EventsCategory.GamePlaing,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Events()
                        {
                            Title = "Game Event Chill",
                            Image = "https://img.freepik.com/free-vector/hand-drawn-people-relaxing-park_52683-69071.jpg?w=1060&t=st=1663766661~exp=1663767261~hmac=a6e6122ea176dbf76e70768d4408f0c0b3923c680200a38c48c45ccc1e51a796",
                            Description = "This is the description of the Chill",
                            EventsCategory = EventsCategory.Chill,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "gorizzont.s@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Dima",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "125 Main St",
                            City = "Sumy",
                            State = "Sumy"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "120 Main St",
                            City = "User",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
}
