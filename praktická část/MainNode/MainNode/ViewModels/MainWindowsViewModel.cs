using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using MainNode.Windows;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace MainNode.ViewModels
{
    public partial class MainWindowsViewModel : ObservableObject
    {
        private readonly FlowRepository _flowRepo;
        private readonly NodeRepository _nodeRepo;
        public MainWindowsViewModel(FlowRepository flowRepo, NodeRepository nodeRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
        }

        [RelayCommand]
        public async Task AddNode()
        {
            App.Current.ShowWindow<AddNodeWindow>();
        }

        [RelayCommand]
        public async Task LoadNodes()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                var json = File.ReadAllText(openFileDialog.FileName);
                var failed = await _nodeRepo.LoadNodes(json);

                foreach (var f in failed)
                {
                    var node = f.Key;
                    var error = f.Value;

                    MessageBox.Show($"Node {node.Name} failed to load: {error}");
                }
            }
        }
    }
}
