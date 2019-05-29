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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for StaffFritids.xaml
    /// </summary>
    public partial class StaffFritids : Window
    {
        List<Child> fritdschildren = new List<Child>();
        public StaffFritids()
        {
            InitializeComponent();
        }

        //private void listViewTotalFritids_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //fritdschildren = DbOperations.GetChildrenAtFritids();
        //    ////listViewTotalFritids.ItemsSource = children;
        //    //listViewTotalFritids.DisplayMemberPath = "Fullinformation";

        //}
    }
}
