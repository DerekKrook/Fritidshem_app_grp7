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
using WpfApp1.Models;
using WpfApp1.Views;

namespace WpfApp1
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

        private void BtnGuardian_Click(object sender, RoutedEventArgs e)
        {
            ListviewGuardian listviewGuardian = new ListviewGuardian();

            listviewGuardian.Show();

            this.Close();
        }

        private void BtnStaff_Click(object sender, RoutedEventArgs e)
        {

            StaffLogin stafflogin = new StaffLogin();
            

            stafflogin.Show();
            
                
            this.Close();
        }
    }
}
