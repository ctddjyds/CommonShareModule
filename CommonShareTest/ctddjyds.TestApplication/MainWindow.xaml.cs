﻿using CTDDJYDS.CommonModule;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ctddjyds.TestApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //NLogHelper.IsEnableLog = true;
            //NLogHelper.Log(LogLevel.Info, "测试日志");
            this.DataContext = new TestPageViewModel();
        }
        private void ShellView_Loaded(object sender, RoutedEventArgs e)
        {
            Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            double dx = m.M11;
            double dy = m.M22;

            ScaleTransform s = (ScaleTransform)mainGrid.LayoutTransform;
            s.ScaleX = 1 / dx;
            s.ScaleY = 1 / dy;
        }
    }
}
