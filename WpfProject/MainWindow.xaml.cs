﻿using System;
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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void OpenWindow(object sender, EventArgs e)
        {
            Type_Window type_Window = new Type_Window();
            type_Window.Show();
        }

        private void OpenWindowSpeech(object sender, EventArgs e)
        {
            Speech_Window speech = new Speech_Window();
            speech.Show();
        }
    }
}
