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
using WpfApp1.DbOperation;
using WpfApp1.Models;
using WpfApp1.Views;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for StaffLogin.xaml
    /// </summary>
    public partial class StaffLogin : Window
    {
               
        public StaffLogin()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {

           // DbOperations db = new DbOperations();
            List<Staff> staff = new List<Staff>();

            //staff = db.GetAllStaff();

            staff = DbOperations.GetAllStaff();


            listViewTotalStaff.ItemsSource = staff;
            listViewTotalStaff.DisplayMemberPath = "Fullinformation";
        }


        private void ListViewTotalStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            // Staff staff = (Staff)listBoxStaff.SelectedItem;
            ListViewStaff staffview = new ListViewStaff();


            staffview.Show();

            this.Close();
        }
    }
}
