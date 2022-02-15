using ClanManagerApi.Models.Security;
using Microsoft.Extensions.Configuration;

namespace ClanManagerApi.Extensions
{
    public static class IConfigurationExtensions
    {
        public static SecurityConfiguration GetSecurityConfiguration(this IConfiguration configuration) =>
            configuration.GetSection(nameof(SecurityConfiguration)).Get<SecurityConfiguration>();
    }
}
