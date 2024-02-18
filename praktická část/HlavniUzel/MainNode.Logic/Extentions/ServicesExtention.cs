using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Logic.Extentions
{
    public static class ServicesExtention
    {
        public static void Logika(this IServiceCollection services)
        {
            Komunikace.Extentions.ServicesExtention.Komunikace(services);
            services.AddTransient<Node>();
            services.AddSingleton<NodeRepository>();
        }
    }
}
