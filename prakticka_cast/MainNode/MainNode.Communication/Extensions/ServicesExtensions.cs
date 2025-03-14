using MainNode.Communication.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Communication.Extensions
{
    public static class ServicesExtensions
    {
        public static void Komunikace(this IServiceCollection services)
        {
            services.AddTransient<INodeCommunication,HttpNodeCommunication>();
        }
    }
}
