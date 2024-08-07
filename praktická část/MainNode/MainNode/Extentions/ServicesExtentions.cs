using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Extentions
{
    public static class ServicesExtentions
    {
        public static void AddServices(this IServiceCollection services)
        {
            MainNode.Logic.Extentions.ServicesExtention.Logika(services);
        }
    }
}
