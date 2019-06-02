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

        // generera id saknas  - hur ska man göra detta smart? man vill ju inte ha så man kan anmäla frånvaro dubbelt? skapa system där id är GCWWDDYY(guardian child week day year)? då blir det unikt och om det redan finns så skriv popup meddelande

        public void DataBinding()
        {
            //Hämta barn
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren.ItemsSource = children;
            comboBoxChildren.DisplayMemberPath = "Fullinformation";

            comboBoxChildren.SelectedIndex = 0;

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
            dates = DbOperations.GetDays();

            comboBoxDay.ItemsSource = dates;
            comboBoxDay.DisplayMemberPath = "InformationDay";
        }

        private void ComboBoxChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren.SelectedItem != null)
            {
                Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);
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

        }

        private void BtnReportAbscence_Click(object sender, RoutedEventArgs e)
        {
            int id = 19;
            string comment = txtbxComment.Text;

            //string week = comboBoxWeek.SelectedItem.ToString();
            //string day = comboBoxDay.SelectedItem.ToString();

            int dates_id = ActiveDate.Id;

            attendances = DbOperations.GuardianReportAttendance(id, comment);

            UpdatedMessage();
        }

        public async void UpdatedMessage()
        {
            lblUpdated.Visibility = Visibility.Visible;
            await Task.Delay(3500);
            lblUpdated.Visibility = Visibility.Hidden;
        }
    }
}
