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
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : Window
    {
        List<Schedule> schedule = new List<Schedule>();
        List<Child> children = new List<Child>();

        public ScheduleView()
        {
            InitializeComponent();

            lblGuardianFirstName.Content = $"Inloggad som {Activeguardian.Firstname} {Activeguardian.Lastname}";

            DataBinding();
        }

        public void DataBinding()
        {
            children = DbOperations.GetChildrenOfGuardian();

            comboBoxChildren.ItemsSource = children;
            comboBoxChildren.DisplayMemberPath = "Fullinformation";

        }

        private void ComboBoxChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxChildren.SelectedItem != null)
            {                
                Activechild.Setactivechild((Child)comboBoxChildren.SelectedItem);       
                
                UpdateSchedule();
            }
            
        }

        public void UpdateSchedule()
        {
            //Vill ha med cateogry_attendance för barn/dag så att det står orsak istället för schema om barnet inte är där och om den får gå hem samma sak med fritids och mat hade varit fin fint :O

            if (comboBoxChildren.SelectedItem != null)
            {
                TabItem tabItem = tabControl.SelectedItem as TabItem;

                string day = tabItem.Header.ToString();

                schedule = DbOperations.GetSchedule(day);
                
                ListViewMonday.ItemsSource = schedule;
                ListViewMonday.DisplayMemberPath = "Fullinformation";
            }
        }

        public void UpdateSchedule(ListView listView)
        {
            //Vill ha med cateogry_attendance för barn/dag så att det står orsak istället för schema om barnet inte är där och om den får gå hem samma sak med fritids och mat hade varit fin fint :O

            if (comboBoxChildren.SelectedItem != null)
            {                            
                TabItem tabItem = tabControl.SelectedItem as TabItem;
                
                string day = tabItem.Header.ToString();

                schedule = DbOperations.GetSchedule(day);
                listView.ItemsSource = schedule;               
                listView.DisplayMemberPath = "Fullinformation";
            }
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
