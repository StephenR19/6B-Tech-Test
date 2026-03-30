using Microsoft.AspNetCore.DataProtection;
using SixBee.Api.Data;
using SixBee.Api.DTOs;
using SixBee.Api.Models;

namespace SixBee.Api.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IDataProtector _protector;

    public AuthService(AppDbContext db, IDataProtectionProvider provider)
    {
        _db = db;
        _protector = provider.CreateProtector("auth-cookie");
    }

    public string? ValidateCredentials(LoginDto dto)
    {
        var admin = _db.Admins.FirstOrDefault(a => a.Email == dto.Email);
        if (admin == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            return null;

        return _protector.Protect(admin.Id.ToString());
    }

    public Admin? GetAdminFromToken(string token)
    {
        try
        {
            var id = int.Parse(_protector.Unprotect(token));
            return _db.Admins.Find(id);
        }
        catch
        {
            return null;
        }
    }
}
