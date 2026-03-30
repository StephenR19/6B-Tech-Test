using SixBee.Api.Models;

namespace SixBee.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Admins.Any())
        {
            context.Admins.Add(new Admin
            {
                Email = "admin@sixbee.co.uk",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
            });
            context.SaveChanges();
        }
    }
}
