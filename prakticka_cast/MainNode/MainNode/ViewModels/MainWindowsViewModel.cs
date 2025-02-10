using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using MainNode.Windows;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using MainNode.Logic.Interfaces;

namespace MainNode.ViewModels
{
    public partial class MainWindowsViewModel : ObservableObject
    {
        private readonly IFlowRepository _flowRepo;
        private readonly INodeRepository _nodeRepo;
        private readonly NodeListViewModel _nodeList;
        private readonly ILoopExecutor _loopExecutor;
        private readonly ILoopCompiler _loopCompiler;
        public MainWindowsViewModel(
            IFlowRepository flowRepo,
            INodeRepository nodeRepo,
            ILoopExecutor loopExecutor,
            ILoopCompiler loopCompiler,
            NodeListViewModel nodeList)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
            _loopExecutor = loopExecutor;
            _loopCompiler = loopCompiler;

            _loopExecutor.LoopFinished += _loopExecutor_LoopFinished;
            _nodeList = nodeList;
        }

        #region nodes
        public NodeListViewModel NodeListViewModel=> _nodeList;
        public void refreshNodes() => _nodeList.refreshNodes();

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
            NodeListViewModel.refreshNodes();
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
            NodeListViewModel.refreshNodes();
        }
        #endregion

        [RelayCommand]
        public async Task ShowFlowEdit()
        {
            App.Current.ShowWindow<FlowEditWindow>();
        }
    }
}
