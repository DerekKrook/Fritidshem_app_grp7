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

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for ListviewGuardian.xaml
    /// </summary>
    public partial class ListviewGuardian : Window
    {
        List<Guardian> guardians = new List<Guardian>();
        List<Child> children = new List<Child>();

        public ListviewGuardian()
        {
            InitializeComponent();
            //UpdateListView();
        }

        private void UpdateListViewGuardian()
        {            
            guardians = DbOperations.GetAllGuardians();

            listViewGuardian.ItemsSource = guardians;
            listViewGuardian.DisplayMemberPath = "Fullinfo";
       
        }

        private void BtnSearchGuardian_Click(object sender, RoutedEventArgs e)
        {
            UpdateListViewGuardian();
        }

        private void ListViewGuardian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Guardian guardian = (Guardian)listViewGuardian.SelectedItem;
            DbOperations.GetChildrenOfGuardian(guardian);

            LoggedInGuardian loggedIn = new LoggedInGuardian();
                      
        }

        private void ListViewGuardian_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateListViewGuardian();
        }
    }
}
