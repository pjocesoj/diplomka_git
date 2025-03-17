using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            MainNode.Logic.Extensions.ServicesExtensions.Logika(services);
        }
    }
}
