using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SixBee.Api.Data;

namespace SixBee.Api.Tests;

public static class TestHelper
{
    public static AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    public static IDataProtectionProvider CreateDataProtectionProvider()
    {
        return DataProtectionProvider.Create("SixBee.Tests");
    }
}
