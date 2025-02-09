using MainNode.Logic.Compile;
using MainNode.Logic.Repos;
using MainNode.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MainNode.Logic.Extentions
{
    public static class ServicesExtention
    {
        public static void Logika(this IServiceCollection services)
        {
            MainNode.Communication.Extentions.ServicesExtention.Komunikace(services);
            services.AddTransient<Node>();
            services.AddSingleton<NodeRepository>();
            services.AddSingleton<IFlowRepository,FlowRepository>();
            services.AddSingleton<LoopExecutor>();
            services.AddSingleton<LoopCompiler>();
            services.AddSingleton<FuncRepo>();
        }
    }
}
