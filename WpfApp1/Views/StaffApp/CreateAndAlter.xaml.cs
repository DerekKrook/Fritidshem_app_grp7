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
    /// Interaction logic for CreateAndAlter.xaml
    /// </summary>
    public partial class CreateAndAlter : Window
    {
        public CreateAndAlter()
        {
            InitializeComponent();
        }



        private void Barn_Loaded(object sender, RoutedEventArgs e) // loadar den varje gång man klickar på taben??
        {
            txtboxEmail.IsEnabled = false;
            lblEmail.IsEnabled = false;
            txtboxPhone.IsEnabled = false;
            lblPhone.IsEnabled = false;
            txtboxGuardian.IsEnabled = true;
            lblGuardian.IsEnabled = true;
            txtboxClass.IsEnabled = true;
            lblClass.IsEnabled = true;
        }

        private void Vårdnadshavare_Loaded(object sender, RoutedEventArgs e) // loadar den varje gång man klickar på taben??
        {
            txtboxEmail.IsEnabled = true;
            lblEmail.IsEnabled = true;
            txtboxPhone.IsEnabled = true;
            lblPhone.IsEnabled = true;
            txtboxGuardian.IsEnabled = false;
            lblGuardian.IsEnabled = false;
            txtboxClass.IsEnabled = false;
            lblClass.IsEnabled = false;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // TXT BOX är du säker på att ta bort?
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            // uppdatera query på vald person beroende på guardian / child?
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            // query lägga till person i lista beroende på guardian / child

            // text om att ny är tillagd

            // uppdatera list
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // tömm alla fält och selecteditem
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblStaffFirstname.Content = $"Inloggad som {Activestaff.Firstname} {Activestaff.Lastname}";

        }
    }
}
