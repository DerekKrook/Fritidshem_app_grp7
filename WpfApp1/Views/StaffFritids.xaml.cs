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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for StaffFritids.xaml
    /// </summary>
    public partial class StaffFritids : Window
    {
        List<Attendance> fritidschildren = new List<Attendance>();
        List<Attendance> fritidsgonehome = new List<Attendance>();

        public StaffFritids()
        {
            InitializeComponent();

        }

        private void ChangeValueLeaveAlone(List list)
        {

        }

        private void ListViewTotalFritids_Loaded(object sender, RoutedEventArgs e)
        {
            fritidschildren = DbOperations.GetChildrenAtFritids();

            foreach (var c in fritidschildren)
            {
                c.UpdateLeaveAlone();
            }

            listViewTotalFritids.ItemsSource = fritidschildren;
           
        }

        private void ListViewFritidsGonehome_Loaded(object sender, RoutedEventArgs e)
        {
            fritidsgonehome = DbOperations.GetChildrenGoneHome();

            foreach (var c in fritidsgonehome)
            {
                c.UpdateLeaveAlone();
            }

            listViewFritidsGonehome.ItemsSource = fritidsgonehome;
          
        }

        private void BtnChildHome_Click(object sender, RoutedEventArgs e)
        {
            DbOperations.SetChildGoneHome();

            fritidschildren = DbOperations.GetChildrenAtFritids();
            listViewTotalFritids.ItemsSource = fritidschildren;
            

            fritidsgonehome = DbOperations.GetChildrenGoneHome();
            listViewFritidsGonehome.ItemsSource = fritidsgonehome;
            
        }

        private void ListViewTotalFritids_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ActiveAttendance.Setactiveattendance((Attendance)listViewTotalFritids.SelectedItem);

            }
            catch (Exception)
            {

                return;
            }
        }

        private void ListViewFritidsGonehome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ActiveAttendance.Setactiveattendance((Attendance)listViewFritidsGonehome.SelectedItem);

            }
            catch (Exception)
            {

                return;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lblStaffFirstname.Content = $"Inloggad som {Activestaff.Firstname} {Activestaff.Lastname}";

        }
    }
}
