using HlavniUzel.Komunikace.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HlavniUzel.Komunikace.Extentions
{
    public static class ServicesExtention
    {
        public static void Komunikace(this IServiceCollection services)
        {
            services.AddTransient<INodeCommunication,HttpNodeCommunication>();
        }
    }
}
