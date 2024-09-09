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
        private readonly LoopExecutor _loopExecutor;
        private readonly LoopCompiler _loopCompiler;
        public MainWindowsViewModel(FlowRepository flowRepo, NodeRepository nodeRepo, LoopExecutor loopExecutor, LoopCompiler loopCompiler)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
            _loopExecutor = loopExecutor;
            _loopCompiler = loopCompiler;

            _loopExecutor.LoopFinished += _loopExecutor_LoopFinished;
        }

        #region nodes
        public List<NodeViewModel> Nodes => _nodeRepo.Nodes.Select(x => new NodeViewModel(x, _nodeRepo, this)).ToList();
        public void refreshNodes() => OnPropertyChanged(nameof(Nodes));

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
            OnPropertyChanged(nameof(Nodes));
        }

        [RelayCommand]
        public async Task SaveNodes()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                var json = _nodeRepo.SaveNodes();
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
        #endregion
        #region loop executor
        public Visibility IsLoopRunning
        {
            get
            {
                return _loopExecutor.IsRunning ? Visibility.Visible : Visibility.Collapsed;
            }
        }


        [RelayCommand]
        public async Task StartLoop()
        {
            _loopExecutor.Start();
            OnPropertyChanged(nameof(IsLoopRunning));
        }
        [RelayCommand]
        public async Task StopLoop()
        {
            _loopExecutor.Stop();
            OnPropertyChanged(nameof(IsLoopRunning));
        }
        private void _loopExecutor_LoopFinished(object? sender, EventArgs e)
        {
        }
        #endregion
    }
}
