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
       
        List<Child> children = new List<Child>();
        List<Guardian> guardian = new List<Guardian>();
        List<Schedule> schedule = new List<Schedule>();

        public ListViewStaff()
        {
            InitializeComponent();

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lblStaffFirstname.Content = $"Inloggad som {Activestaff.Firstname} {Activestaff.Lastname}";

            ListViewStaff1_Class1.ItemsSource = DbOperations.GetFirstGraders(1);
            ListViewStaff1_Class1.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Class2.ItemsSource = DbOperations.GetFirstGraders(2);
            ListViewStaff1_Class2.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Class3.ItemsSource = DbOperations.GetFirstGraders(4);
            ListViewStaff1_Class3.DisplayMemberPath = "Fullinformation";

            ListViewStaff1_Abscense.ItemsSource = DbOperations.GetSickAndHolidayChildren();
            
        }

      private void getguardian()
        {
         listViewGuardian.ItemsSource = guardian;
         listViewGuardian.DisplayMemberPath = "Fullinfo";
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
        private void BtnEmptySearch_Click(object sender, RoutedEventArgs e)
        {          
            ListViewStaff1.Visibility = Visibility.Hidden;
            btnEmptySearch.Visibility = Visibility.Hidden;

            listViewGuardian.ItemsSource = null;
            ListViewStaff1.ItemsSource = null;

        }

        private void UpdateChildLabel()
        {
            lblActiveChild.Content = $"{Activechild.Firstname} {Activechild.Lastname}";
            lblActiveChild2.Content = $"{Activechild.Firstname} {Activechild.Lastname}";
        }
        private void BtnAbscense_Click(object sender, RoutedEventArgs e)
        {

            StaffReportAbsence staffreportabsence = new StaffReportAbsence();

            staffreportabsence.Show();

            this.Close();
        }

        private void ListViewStaff1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)ListViewStaff1.SelectedItem);
            
            guardian = DbOperations.GetGuardianOfChild(Activechild.Id);
            getguardian();
            UpdateSchedule();
            UpdateChildLabel();

        }

        private void ListViewStaff1_Class1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)ListViewStaff1_Class1.SelectedItem);

            guardian = DbOperations.GetGuardianOfChild(Activechild.Id);
            getguardian();
            UpdateSchedule();
            UpdateChildLabel();
        }
        private void ListViewStaff1_Class2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)ListViewStaff1_Class2.SelectedItem);

            guardian = DbOperations.GetGuardianOfChild(Activechild.Id);
            getguardian();
            UpdateSchedule();
            UpdateChildLabel();

        }
        private void ListViewStaff1_Class3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)ListViewStaff1_Class3.SelectedItem);

            guardian = DbOperations.GetGuardianOfChild(Activechild.Id);
            getguardian();
            UpdateSchedule();
            UpdateChildLabel();
        }


        public void UpdateSchedule()
        {
                TabItem tabItem = tabSchedule.SelectedItem as TabItem;

                string day = tabItem.Header.ToString();

                schedule = DbOperations.GetSchedule(day);

                ListViewMonday.ItemsSource = schedule;
                ListViewMonday.DisplayMemberPath = "Fullinformation";
            
        }

        public void UpdateSchedule(ListView listView)
        {

                TabItem tabItem = tabSchedule.SelectedItem as TabItem;

                string day = tabItem.Header.ToString();

                schedule = DbOperations.GetSchedule(day);
                listView.ItemsSource = schedule;
                listView.DisplayMemberPath = "Fullinformation";
            
        }
        private void Tuesday_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSchedule(ListViewTuesday);
        }

        private void Monday_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSchedule(ListViewMonday);

        }

        private void Wednesday_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSchedule(ListViewWednesday);

        }

        private void Thursday_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSchedule(ListViewThursday);

        }

        private void Friday_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateSchedule(ListViewFriday);

        }


    }
}
