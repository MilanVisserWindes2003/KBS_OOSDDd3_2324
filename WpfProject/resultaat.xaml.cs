﻿using Business;
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
using System.Windows.Threading;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for resultaat.xaml
    /// </summary>
    public partial class resultaat : Page
    {
        Business.Business business;
        

        public resultaat(Business.Business business)
        {
            
            InitializeComponent();
            this.DataContext = this;
            this.business = business;
            timerResult.Text = business.GetTimerText();
        }
        
    }
}
