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

        public Reportabscence()
        {
            InitializeComponent();

            DataBinding();
        }

        //Tänk en formulär med flera comboboxar där man väljer barn, frånvaro anlednning, datum och sen en textbox med kommentar :D

        public void DataBinding()
        {
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren.ItemsSource = children;
            comboBoxChildren.DisplayMemberPath = "Fullinformation";

            comboBoxChildren.SelectedIndex = 0;


            attendancecategories = DbOperations.GetAttendances();

            comboBoxAbscence.ItemsSource = attendancecategories;
            comboBoxAbscence.DisplayMemberPath = "Fullinformation";

            comboBoxAbscence.SelectedIndex = 0;
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

        private void BtnReportAbscence_Click(object sender, RoutedEventArgs e)
        {
            int id  = 1;
            string comment = txtbxComment.Text;
            string day = "måndag";

           attendances = DbOperations.GuardianReportAttendance(id, comment, day);
        }
    }
}
