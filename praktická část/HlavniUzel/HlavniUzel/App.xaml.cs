using HlavniUzel.Extentions;
using HlavniUzel.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HlavniUzel
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                 .ConfigureServices((context, services) =>
                 {
                     services.AddServices();
                     services.AddSingleton<MainWindow>();
                     services.AddTransient<AddNodeWindow>();
                 })
                 .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host!.StartAsync();

            var startupForm = _host.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        public void ShowWindow<T>()where T : Window
        {
            var Form = _host.Services.GetRequiredService<T>();
            Form.Show();
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
