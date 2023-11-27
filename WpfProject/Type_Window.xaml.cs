using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for Type_Window.xaml
    /// </summary>
    
    public partial class Type_Window : Window
    {
        public string Text {  get; set; }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                }
            }
        }
        public Type_Window()
        {  
            InitializeComponent();
            DataContext = this;
            Text = "test";
        }
    }
}
