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

        public ListviewGuardian()
        {
            InitializeComponent();
            //UpdateListView();
        }

        //private void UpdateListView()
        //{
        //    DbOperations db = new DbOperations();

        //    listViewGuardian.ItemsSource = db.GetGuardian(txtLastNameGuardian.Text); 
        //}

        private void BtnSearchGuardian_Click(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            guardians = db.GetGuardian(txtLastNameGuardian.Text);

            listViewGuardian.ItemsSource = guardians;
            listViewGuardian.DisplayMemberPath = "Fullinfo";
        }
    }
}
