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
    /// Interaction logic for Reportfritids.xaml
    /// </summary>
    public partial class Reportfritids : Window
    {
        List<Child> children = new List<Child>();
        List<Attendance> attendances = new List<Attendance>();
        List<Date> dates = new List<Date>();
        List<Weeks> weeks = new List<Weeks>();

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
            string comment = txtbxComment.Text;

            int classid = 3;

            attendances = DbOperations.GuardianReportFritids(comment, classid);

            UpdatedMessage();
        }

        public async void UpdatedMessage()
        {
            lblUpdated.Visibility = Visibility.Visible;
            await Task.Delay(3500);
            lblUpdated.Visibility = Visibility.Hidden;
        }

        private void Seereports_Loaded(object sender, RoutedEventArgs e)
        {
            attendances = DbOperations.Getfritidsguardian();

            ListView.ItemsSource = attendances;
        }
    }
}
