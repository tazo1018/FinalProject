using FinalProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public static class DataSeeder
    {
        public static void SeedPosts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "რალამაზი დღეა",
                    CreationTime = DateTime.Now,
                    AuthorId = "8716671C-1D98-48ED-83D0-F859C8F88D31", 
                    State = State.Show,
                    Status = Status.Active,
                    Description = "გამარჯობა, მართლააც რომ ლამაზი დღეა"
                }
            );
        }


        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "AE7F1D3D-B8DC-4078-947D-6668F464EB96", Name = "administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Id = "B72F34A4-F35C-49F4-9600-490BF91D72E9", Name = "user", NormalizedName = "USER" }
            );
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            PasswordHasher<IdentityUser> hasher = new();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "A242E583-FAE8-4F98-BA9A-78424AA8ADCF",
                    UserName = "AdminUser",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    PhoneNumber = "555337681",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "admin",
                    LastName = "admin",
                    IsBlocked = false,
                },
                new User
                {
                    Id = "DB7C8863-E13B-4E92-AAB0-34268994E951",
                    UserName = "nika@gmail.com",
                    NormalizedUserName = "NIKA@GMAIIL.COM",
                    Email = "nika@gmail.com",
                    NormalizedEmail = "NIKA@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "nika23!"),
                    PhoneNumber = "559337281",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "nika",
                    LastName = "nika",
                    IsBlocked = false,
                },
                new User
                {
                    Id = "0B584E72-528B-4D18-8230-47120C66027F",
                    UserName = "gio@gmail.com",
                    NormalizedUserName = "GIO@GMAIL.COM",
                    Email = "gio@gmail.com",
                    NormalizedEmail = "GIO@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "giogio123!"),
                    PhoneNumber = "591352681",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "gio",
                    LastName = "gio",
                    IsBlocked = false,
                },
                new User 
                {
                    Id = "8716671C-1D98-48ED-83D0-F859C8F88D31",
                    UserName = "AuthorUser",
                    NormalizedUserName = "AUTHOR@GMAIL.COM",
                    Email = "author@gmail.com",
                    NormalizedEmail = "AUTHOR@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(null, "Author123!"),
                    PhoneNumber = "5555555555",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "author",
                    LastName = "user",
                    IsBlocked = false,
                }
            );
        }


        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "AE7F1D3D-B8DC-4078-947D-6668F464EB96", UserId = "A242E583-FAE8-4F98-BA9A-78424AA8ADCF" },
                new IdentityUserRole<string> { RoleId = "B72F34A4-F35C-49F4-9600-490BF91D72E9", UserId = "DB7C8863-E13B-4E92-AAB0-34268994E951" },
                new IdentityUserRole<string> { RoleId = "B72F34A4-F35C-49F4-9600-490BF91D72E9", UserId = "0B584E72-528B-4D18-8230-47120C66027F" }
            );
        }
    }
}
