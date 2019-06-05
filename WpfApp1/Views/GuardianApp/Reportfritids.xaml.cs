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
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Reportfritids.xaml
    /// </summary>
    public partial class Reportfritids : Window
    {
        List<Child> children = new List<Child>();
        List<Attendance> attendances = new List<Attendance>();
        List<Date> dates = new List<Date>();
        List<Weeks> weeks = new List<Weeks>();
        List<Attendancecategory> attendancecategories = new List<Attendancecategory>();
        List<Meal> meals = new List<Meal>(); 

        public Reportfritids()
        {
            InitializeComponent();

            DataBinding();
        }

        public void DataBinding()
        {
            //Hämta barn
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren.ItemsSource = children;
            comboBoxChildren.DisplayMemberPath = "Fullinformation";

            comboBoxChildren.SelectedIndex = 0;

            comboBoxChildMeals.ItemsSource = children;
            comboBoxChildMeals.DisplayMemberPath = "Fullinformation";
            comboBoxChildMeals.SelectedIndex = 0;

            //Hämta barn se
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren2.ItemsSource = children;
            comboBoxChildren2.DisplayMemberPath = "Fullinformation";

            comboBoxChildren2.SelectedIndex = 0;

            //Hämta veckor
            weeks = DbOperations.GetWeek();

            comboBoxWeek.ItemsSource = weeks;
            comboBoxWeek.DisplayMemberPath = "InformationWeek";

            //Hämta dagar
            dates = DbOperations.GetDays();

            comboBoxDay.ItemsSource = dates;
            comboBoxDay.DisplayMemberPath = "InformationDay";

            //Hämta Morgon/Kväll
            attendancecategories = DbOperations.GetFritidsMorningEvening();

            comboBoxType.ItemsSource = attendancecategories;
            comboBoxType.DisplayMemberPath = "Fullinformation";
        }

        private void GetMeals()
        {
            meals = DbOperations.GetMeals();
            ListView.ItemsSource = null;
            ListViewMeals.Items.Refresh();
            ListViewMeals.ItemsSource = meals;
        }

        private void GetAttendances()
        {
            attendances = DbOperations.Getfritidsguardian();
            ListView.ItemsSource = null;
            ListView.Items.Refresh();
            ListView.ItemsSource = attendances;
        }

        private void UpdateListView()
        {
            ListView.Items.Refresh();            
        }


        private void ComboBoxChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren.SelectedItem != null)
            {
                Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);
            }
        }

        private void ComboBoxChildren2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren2.SelectedItem != null)
            {
                Activechild.Setactivechild((Child)comboBoxChildren2.SelectedItem);
                
                GetAttendances();
            }
        }

        private void ComboBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxDay.SelectedItem != null)
            {
                ActiveDate.Setactivatedate((Date)comboBoxDay.SelectedItem);
            }
        }

        private void ComboBoxWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnReportAbscence_Click(object sender, RoutedEventArgs e)
        {
            int i = comboBoxType.SelectedIndex;
            string comment = txtbxComment.Text;
            int attendanceid = 0;

            Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);

            try
            {
                if (chxbxBreakfast.IsChecked == true)
                {

                    if (i == 0)
                    {
                        attendanceid = 7;
                        DbOperations.GuardianReportFritidsBreakfast(comment, attendanceid);

                        attendanceid = 3;
                        DbOperations.GuardianReportFritids(comment, attendanceid);

                    }

                    if (i == 1)
                    {

                        attendanceid = 7;
                        DbOperations.GuardianReportFritidsBreakfast(comment, attendanceid);

                    }

                }
                else
                {

                    if (i == 2)
                    {

                        attendanceid = 3;
                        DbOperations.GuardianReportFritids(comment, attendanceid);

                    }

                    else if (i == 1)
                    {
                        attendanceid = 7;
                        DbOperations.GuardianReportFritids(comment, attendanceid);
                    }

                    else if (i == 0)
                    {
                        attendanceid = 7;
                        DbOperations.GuardianReportFritids(comment, attendanceid);

                        attendanceid = 3;
                        DbOperations.GuardianReportFritids(comment, attendanceid);
                    }

                }
                UpdatedMessage();
                GetMeals();
                GetAttendances();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public async void UpdatedMessage()
        {
            lblUpdated.Visibility = Visibility.Visible;
            await Task.Delay(3500);
            lblUpdated.Visibility = Visibility.Hidden;
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int i = comboBoxType.SelectedIndex;

            if (i == 0)
            {
                chxbxBreakfast.IsEnabled = true;
                chxbxBreakfast.Visibility = Visibility.Visible;
            }
            else if (i == 1)
            {
                chxbxBreakfast.IsEnabled = true;
                chxbxBreakfast.Visibility = Visibility.Visible;
            }
            else if (i == 2)
            {
                chxbxBreakfast.IsEnabled = false;
                chxbxBreakfast.IsChecked = false;
                chxbxBreakfast.Visibility = Visibility.Hidden;
            }
        }

        private void Seereports_Loaded_1(object sender, RoutedEventArgs e)
        {

            GetAttendances(); 
            
        }

        private void Seereportedmeals_Loaded(object sender, RoutedEventArgs e)
        {
           
            GetMeals();

        }

        private void ComboBoxChildMeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)comboBoxChildMeals.SelectedItem);

            GetMeals();
        }
    }
}
