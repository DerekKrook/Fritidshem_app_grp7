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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for StaffMenu.xaml
    /// </summary>
    public partial class StaffMenu : UserControl
    {
        public StaffMenu()
        {
            InitializeComponent();
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            window.Close();

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            ListViewStaff listViewStaff = new ListViewStaff();

            listViewStaff.Show();

            window.Close();
        }

        private void Fritids_Click(object sender, RoutedEventArgs e)
        {

            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            StaffFritids staffFritids = new StaffFritids();

            staffFritids.Show();

            window.Close();

        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.WindowState = WindowState.Minimized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            window.Close();
        }
    }
}
