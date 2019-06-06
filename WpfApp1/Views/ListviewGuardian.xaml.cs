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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ListviewGuardian.xaml
    /// </summary>
    public partial class ListviewGuardian : Window
    {
        List<Guardian> guardians = new List<Guardian>();
        

        public ListviewGuardian()
        {
            InitializeComponent();
        }

        private void UpdateListViewGuardian()
        {            
            guardians = DbOperations.GetAllGuardians();
            listViewGuardian.Items.Refresh();
            listViewGuardian.ItemsSource = guardians;
                  
        }

        private void BtnSearchGuardian_Click(object sender, RoutedEventArgs e)
        {
            guardians = DbOperations.GetGuardian(txtLastNameGuardian.Text);
            listViewGuardian.Items.Refresh();
            listViewGuardian.ItemsSource = guardians;
        }

        private void ListViewGuardian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Activeguardian.Setactiveguardian((Guardian)listViewGuardian.SelectedItem); // tilldelar objektet activeguardian vald person i listan

            ScheduleView scheduleView = new ScheduleView();

            scheduleView.Show();

            this.Close();
                      
        }

        private void ListViewGuardian_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateListViewGuardian();
        }
    }
}
