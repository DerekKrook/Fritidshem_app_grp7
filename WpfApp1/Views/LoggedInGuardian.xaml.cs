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
        List<Child> children = new List<Child>();
        List<Schedule> schedule = new List<Schedule>();

        public LoggedInGuardian()
        {
            
           InitializeComponent();

           lblWelcome.Content = "Välkommen till fritidshem";
           lblGuardianFirstName.Content = $"{Activeguardian.Firstname} {Activeguardian.Lastname}";

            children = DbOperations.GetChildrenOfGuardian();

            listViewGuardianChild.Items.Clear();
            listViewGuardianChild.ItemsSource = children;
            listViewGuardianChild.DisplayMemberPath = "Fullinformation";
        }
        
        private void ListViewGuardianChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)listViewGuardianChild.SelectedItem);
        }
    }
}
