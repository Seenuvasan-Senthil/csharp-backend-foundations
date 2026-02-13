using Microsoft.Extensions.Configuration;

public static class ConfigurationHelper
{
    public static string GetConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        return config.GetConnectionString("DefaultConnection");
    }
}
