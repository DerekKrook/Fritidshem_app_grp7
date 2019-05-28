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
using WpfApp1.Views;



namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for LoggedInGuardian.xaml
    /// </summary>
    public partial class LoggedInGuardian : Window
    {
      
        public LoggedInGuardian(List <Child> children, Guardian guardian)
        {
            InitializeComponent();
           
            lblGuardianFirstName.Content = $"Inloggad som {guardian.Firstname} {guardian.Lastname}";

            listViewGuardianChild.Items.Clear();
            listViewGuardianChild.ItemsSource = children;
            listViewGuardianChild.DisplayMemberPath = "Fullinformation";
        }
        
    }
}
