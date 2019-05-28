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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GuardianMenu.xaml
    /// </summary>
    public partial class GuardianMenu : UserControl
    {
        public GuardianMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_LogOut(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            window.Close();

        }

        private void MenuItem_Click_Profile(object sender, RoutedEventArgs e)
        {

        }
    }
}
