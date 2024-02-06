using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HlavniUzel.Controls
{

    public partial class LabelTextBoxControl : UserControl
    {
        public LabelTextBoxControl()
        {
            InitializeComponent();
            label1.DataContext = this;
        }

        #region label text
        public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(LabelTextBoxControl), new UIPropertyMetadata("???"));
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        #endregion

        #region TextBox text
        public static readonly DependencyProperty TextBoxTextProperty =
        DependencyProperty.Register(nameof(TextBoxText), typeof(string), typeof(LabelTextBoxControl), new UIPropertyMetadata("???"));
        public string TextBoxText
        {
            get { return (string)GetValue(TextBoxTextProperty); }
            set { SetValue(TextBoxTextProperty, value); }
        }
        #endregion
    }
}
