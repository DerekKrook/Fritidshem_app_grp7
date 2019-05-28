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
    /// Interaction logic for ListViewStaff.xaml
    /// </summary>
    public partial class ListViewStaff : Window
    {
        //DbOperations db = new DbOperations();
        List<Child> children = new List<Child>();
        List<Guardian> guardian = new List<Guardian>();

        public ListViewStaff()
        {
            InitializeComponent();
        }

        private void BtnSearchChild_Click(object sender, RoutedEventArgs e)
        {
            

            children = DbOperations.GetChildren(txtNameChild.Text);


            ListViewStaff1.ItemsSource = children;
            ListViewStaff1.DisplayMemberPath = "Fullinformation";
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            children = DbOperations.GetAllChildren();

            ListViewStaff1.ItemsSource = children;
            ListViewStaff1.DisplayMemberPath = "Fullinformation";
        }

        private void ListViewStaff1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Child child = (Child)ListViewStaff1.SelectedItem;
            guardian = DbOperations.GetGuardianOfChild(child);

            
            listViewGuardian.ItemsSource = guardian;
            listViewGuardian.DisplayMemberPath = "Fullinfo";
        }
    }
}
