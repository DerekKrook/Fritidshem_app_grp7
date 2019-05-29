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
using WpfApp1;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoggedInGuardian.xaml
    /// </summary>
    public partial class LoggedInGuardian : Window
    {
        List<Guardian> guardians = new List<Guardian>();
        List<Child> children = new List<Child>();
        List<Schedule> schedule = new List<Schedule>();

        public LoggedInGuardian(List<Child>children, Guardian guardian)
        {
            
            InitializeComponent();
           
            lblGuardianFirstName.Content = $"Inloggad som {guardian.Firstname} {guardian.Lastname}";

            listViewGuardianChild.Items.Clear();
            listViewGuardianChild.ItemsSource = children;
            listViewGuardianChild.DisplayMemberPath = "Fullinformation";
        }

        private void BtnScheduleChild_Click(object sender, RoutedEventArgs e)
        {           
            Child child = (Child)listViewGuardianChild.SelectedItem;
            if(child != null)
            {
                schedule = DbOperations.GetSchedule(child);
                listViewSchedule.ItemsSource = schedule;
                listViewSchedule.DisplayMemberPath = "Fullinformation";
            }
        }
    }
}
