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
            services.AddSingleton<INodeRepository, NodeRepository>();
            services.AddSingleton<IFlowRepository, FlowRepository>();
            services.AddSingleton<ILoopExecutor, LoopExecutor>();
            services.AddSingleton<ILoopCompiler, LoopCompiler>();
            services.AddSingleton<FuncRepo>();
        }
    }
}
