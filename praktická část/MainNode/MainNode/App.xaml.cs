using MainNode.Extentions;
using MainNode.ViewModels;
using MainNode.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MainNode
{
    public partial class App : Application
    {
        //? a ! použity jen abych umlčel warning
        public static new App Current => _current!;
        private static App? _current;

        public static IServiceProvider Services { get { return Current._host.Services; } }

        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                 .ConfigureServices((context, services) =>
                 {
                     services.AddServices();
                     services.AddSingleton<MainWindow>();
                     services.AddSingleton<MainWindowsViewModel>();

                     services.AddTransient<AddNodeWindow>();
                     services.AddTransient<NodeViewModel>();

                     services.AddTransient<NodeInfoWindow>();
                     services.AddSingleton<NodeListViewModel>();

                     services.AddSingleton<FlowEditWindow>();
                     services.AddSingleton<FlowEditWindowViewModel>();
                 })
                 .Build();

            _current = this;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host!.StartAsync();

            var startupForm = _host.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        public void ShowWindow<T>(object? viewModel = null) where T : Window
        {
            var Form = _host.Services.GetRequiredService<T>();
            Form.Show();

            if (viewModel != null) { Form.DataContext = viewModel; }
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }

}
