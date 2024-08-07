﻿using MainNode.ViewModels;
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
    /// Interakční logika pro NodeInfoWindow.xaml
    /// </summary>
    public partial class NodeInfoWindow : Window
    {
        public NodeInfoWindow()
        {
            InitializeComponent();
        }
        public NodeInfoWindow(NodeViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
