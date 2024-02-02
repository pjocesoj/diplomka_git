using Microsoft.Extensions.DependencyInjection;

namespace HlavniUzel.Logika.Extentions
{
    public static class ServicesExtention
    {
        public static void Logika(this IServiceCollection services)
        {
            Komunikace.Extentions.ServicesExtention.Komunikace(services);
        }
    }
}
