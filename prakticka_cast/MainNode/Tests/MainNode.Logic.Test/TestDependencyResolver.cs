using Microsoft.Extensions.DependencyInjection;
using MainNode.Logic.Extentions;
using MainNode.Logic.Interfaces;
using MainNode.Logic.Repos;
using Microsoft.Extensions.Hosting;
using MainNode.Logic.Compile;

namespace MainNode.Logic.Test
{
    public class TestDependencyResolver
    {
        private static readonly IHost _host;

        static TestDependencyResolver()
        {
            _host = Host.CreateDefaultBuilder()
                 .ConfigureServices((context, services) =>
                 {
                     services.AddTransient<Node>();
                     services.AddTransient<INodeRepository, NodeRepository>();
                     services.AddTransient<IFlowRepository, FlowRepository>();
                     services.AddTransient<LoopExecutor>();
                     services.AddTransient<LoopCompiler>();
                     services.AddTransient<FuncRepo>();
                 })
                 .Build();
        }

        public static T Resolve<T>()
        {
            return _host.Services.GetRequiredService<T>();
        }
    }
}
