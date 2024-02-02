using HlavniUzel.Extentions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HlavniUzel
{ 
    public partial class App : Application
    {
        IHost _host;
        public App()
        {
           HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Services.AddServices();
            builder.Services.AddSingleton<MainWindow>();

            _host = builder.Build();           
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var startupForm = _host.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }
    }

}
