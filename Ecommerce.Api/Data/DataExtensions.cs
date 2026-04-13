using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
        dbContext.Database.Migrate();
    }

    public static void AddEcommerceDb(this WebApplicationBuilder builder)
    {   
        var dbConnString = builder.Configuration.GetConnectionString("EcommerceDb"); 
        builder.Services.AddScoped<EcommerceContext>();
        builder.Services.AddNpgsql<EcommerceContext>(dbConnString);
    }
}
