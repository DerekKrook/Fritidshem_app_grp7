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
        List<Attendance> fritdschildren = new List<Attendance>();
        List<Attendance> fritidsgonehome = new List<Attendance>();
        public StaffFritids()
        {
            InitializeComponent();
        }

        private void ListViewTotalFritids_Loaded(object sender, RoutedEventArgs e)
        {
            fritdschildren = DbOperations.GetChildrenAtFritids();
            listViewTotalFritids.ItemsSource = fritdschildren;
            listViewTotalFritids.DisplayMemberPath = "Fullinformation";
        }

        private void ListViewFritidsGonehome_Loaded(object sender, RoutedEventArgs e)
        {
            fritidsgonehome = DbOperations.GetChildrenGoneHome();
            listViewFritidsGonehome.ItemsSource = fritdschildren;
            listViewFritidsGonehome.DisplayMemberPath = "Fullinformation";
        }

        private void BtnChildHome_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
