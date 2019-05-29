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
    /// Interaction logic for ListViewStaff.xaml
    /// </summary>
    public partial class ListViewStaff : Window
    {
        //DbOperations db = new DbOperations();
        List<Child> children = new List<Child>();
        List<Guardian> guardian = new List<Guardian>();
        List<Child> getclassone = new List<Child>();
        List<Child> getclasstwo = new List<Child>();
        List<Child> getclassthree = new List<Child>();

        public ListViewStaff()
        {
            InitializeComponent();
        }

        private void BtnSearchChild_Click(object sender, RoutedEventArgs e)
        {
            
            ListViewStaff1.Visibility = Visibility.Visible; 
            btnEmptySearch.Visibility = Visibility.Visible;
            children = DbOperations.GetChildren(txtNameChild.Text);
            txtNameChild.Clear();
            ListViewStaff1.ItemsSource = children;
            ListViewStaff1.DisplayMemberPath = "Fullinformation";

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            children = DbOperations.GetAllChildren();
            getclassone = DbOperations.GetFirstGraders();
            getclasstwo = DbOperations.GetSecondGraders();
            getclassthree = DbOperations.GetThirdGraders();

            ListViewStaff1.ItemsSource = children;
            ListViewStaff1.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Class1.ItemsSource = getclassone;
            ListViewStaff1_Class1.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Class2.ItemsSource = getclasstwo;
            ListViewStaff1_Class2.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Class3.ItemsSource = getclassthree;
            ListViewStaff1_Class3.DisplayMemberPath = "Fullinformation";
        }


        private void ListViewStaff1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        Child child = (Child)ListViewStaff1.SelectedItem;
            if (child!= null)
            {
                guardian = DbOperations.GetGuardianOfChild(child);
                listViewGuardian.ItemsSource = guardian;
                listViewGuardian.DisplayMemberPath = "Fullinfo";
            }
    }

        private void BtnEmptySearch_Click(object sender, RoutedEventArgs e)
        {
            ListViewStaff1.Visibility = Visibility.Hidden;
            btnEmptySearch.Visibility = Visibility.Hidden;
            
            listViewGuardian.ItemsSource = null;
            children = DbOperations.GetAllChildren();

            ListViewStaff1.ItemsSource = children;
            ListViewStaff1.DisplayMemberPath = "Fullinformation";
        }

        private void ListViewStaff1_Class2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Child childclass2 = (Child)ListViewStaff1_Class2.SelectedItem;
            if (childclass2 != null)
            {
                guardian = DbOperations.GetGuardianOfChild(childclass2);
                listViewGuardian.ItemsSource = guardian;
                listViewGuardian.DisplayMemberPath = "Fullinfo";
            }

        }

        private void ListViewStaff1_Class1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Child childclass1 = (Child)ListViewStaff1_Class1.SelectedItem;
            if (childclass1 != null)
            {
                guardian = DbOperations.GetGuardianOfChild(childclass1);
                listViewGuardian.ItemsSource = guardian;
                listViewGuardian.DisplayMemberPath = "Fullinfo";
            }

        }

        private void ListViewStaff1_Class3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Child childclass3 = (Child)ListViewStaff1_Class3.SelectedItem;
            if (childclass3 != null)
            {
                guardian = DbOperations.GetGuardianOfChild(childclass3);
                listViewGuardian.ItemsSource = guardian;
                listViewGuardian.DisplayMemberPath = "Fullinfo";
            }

        }
    }
}
