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

namespace MainNode.Controls
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
        DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(LabelTextBoxControl), new UIPropertyMetadata("???",LabelTextChangedCallback));
        private static void LabelTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabelTextBoxControl control = (LabelTextBoxControl)d;
            control.LabelText = (e.NewValue as string);

            var context = control.label1.DataContext;
            if (context != null)
            {
                control.label1.Content = control.LabelText;
            }
        }
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        #endregion

        bool _init = false;

        #region TextBox text
        public static readonly DependencyProperty TextBoxTextProperty =
        DependencyProperty.Register(nameof(TextBoxText), typeof(string), typeof(LabelTextBoxControl), new UIPropertyMetadata("???", TextBoxTextChangedCallback));
        private static void TextBoxTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabelTextBoxControl control = (LabelTextBoxControl)d;
            control.TextBoxText = (e.NewValue as string);

            var context = control.textBox1.DataContext;
            if (!control._init && context != null)
            {
                control._init = true;
                control.textBox1.Text = control.TextBoxText;
            }
        }

        public string TextBoxText
        {
            get { return (string)GetValue(TextBoxTextProperty); }
            set { SetValue(TextBoxTextProperty, value); }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxText = textBox1.Text;
        }
        #endregion
    }
}
