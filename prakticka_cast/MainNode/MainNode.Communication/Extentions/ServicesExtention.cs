using MainNode.Communication.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Communication.Extentions
{
    public static class ServicesExtention
    {
        public static void Komunikace(this IServiceCollection services)
        {
            services.AddTransient<INodeCommunication,HttpNodeCommunication>();
        }
    }
}
