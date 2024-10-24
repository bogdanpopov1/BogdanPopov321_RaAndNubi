using System;
using System.Linq;
using System.Windows;
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;
using RaAndNubi.Pages;
using System.IO;

namespace RaAndNubi
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new AuthorizationPage());
        }
    }
}
