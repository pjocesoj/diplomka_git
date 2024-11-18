using MainNode.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainNode.Windows
{
    /// <summary>
    /// Interaction logic for FlowEditWindow.xaml
    /// </summary>
    public partial class FlowEditWindow : Window
    {
        public FlowEditWindow(FlowEditWindowViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
