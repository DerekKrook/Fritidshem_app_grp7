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
    /// Interaction logic for StaffReportAbsence.xaml
    /// </summary>
    public partial class StaffReportAbsence : Window
    {
        List<Attendance> attendances = new List<Attendance>();
        List<Attendancecategory> attendancecategories = new List<Attendancecategory>();
        List<Date> dates = new List<Date>();
        List<Weeks> weeks = new List<Weeks>();

        public StaffReportAbsence()
        {
            InitializeComponent();
        }

        public void DataBinding()
        {

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

        }


        private void ComboBoxAbscence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxAbscence.SelectedItem != null)
            {
                ActiveAttendancecategory.Set((Attendancecategory)comboBoxAbscence.SelectedItem);
            }
        }

        private void ComboBoxWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxDay.SelectedItem != null)
            {
                ActiveDate.Setactivatedate((Date)comboBoxDay.SelectedItem);
            }
        }

        private void BtnReportAbscence_Click(object sender, RoutedEventArgs e)
        {
            string comment = txtbxComment.Text;

            DbOperations.StaffReportAttendance(comment);

           
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lblActiveChild.Content = $"{Activechild.Firstname} {Activechild.Lastname}";
            DataBinding();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            ListViewStaff listViewStaff = new ListViewStaff();

            listViewStaff.Show();

            window.Close();
        }
    }
}
