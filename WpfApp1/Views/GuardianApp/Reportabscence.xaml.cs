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
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Reportabscence.xaml
    /// </summary>
    public partial class Reportabscence : Window
    {
        List<Child> children = new List<Child>();
        List<Attendance> attendances = new List<Attendance>();
        List<Attendancecategory> attendancecategories = new List<Attendancecategory>();
        List<Date> dates = new List<Date>();
        List<Weeks> weeks = new List<Weeks>();
        

        public Reportabscence()
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

            //Hämta barn se
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren2.ItemsSource = children;
            comboBoxChildren2.DisplayMemberPath = "Fullinformation";

            comboBoxChildren2.SelectedIndex = 0;

            //Hämta frånvaro typer
            attendancecategories = DbOperations.GetAttendances();

            comboBoxAbscence.ItemsSource = attendancecategories;
            comboBoxAbscence.DisplayMemberPath = "Fullinformation";

            comboBoxAbscence.SelectedIndex = 0;

            //Hämta veckor
            weeks = DbOperations.GetWeek();

            comboBoxWeek.ItemsSource = weeks;
            comboBoxWeek.DisplayMemberPath = "InformationWeek";

            //Hämta dagar
            Weeks week = new Weeks();
            week.Week = 1;
            dates = DbOperations.GetDays(week);
            comboBoxDay.ItemsSource = dates;
            comboBoxDay.DisplayMemberPath = "InformationDay";
            
            Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);
        }

        private void UpdateCombobox(ComboBox comboBox, ComboBox combo)
        {
            comboBox.Text = combo.Text;
        }

        private void GetAttendances()
        {
            attendances = DbOperations.Getabscenceasguardian();
            ListView.Items.Refresh();
            ListView.ItemsSource = attendances;
        }

        private void SetActiveChild(ComboBox comboBox)
        {
            Activechild.Setactivechild((Child)comboBox.SelectedItem);
           
        }


        private void ComboBoxChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren.SelectedItem != null)
            {
                SetActiveChild(comboBoxChildren);
                GetAttendances();
            }
        }

        private void ComboBoxChildren2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren2.SelectedItem != null)
            {
                SetActiveChild(comboBoxChildren2);
            }
        }

        private void ComboBoxAbscence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxAbscence.SelectedItem != null)
            {
                ActiveAttendancecategory.Set((Attendancecategory)comboBoxAbscence.SelectedItem);
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
            if (comboBoxWeek.SelectedItem != null)
            {
                Weeks week = (Weeks)comboBoxWeek.SelectedItem;
                dates = DbOperations.GetDays(week);
                comboBoxDay.ItemsSource = dates;
                comboBoxDay.DisplayMemberPath = "InformationDay";
            }
        }

        private void BtnReportAbscence_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string comment = txtbxComment.Text;
                DbOperations.GuardianReportAttendance(comment);
                UpdatedMessage();
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

        private void Seereports_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveChild(comboBoxChildren2);
            GetAttendances();           
        }


    }
}
