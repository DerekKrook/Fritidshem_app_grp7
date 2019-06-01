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

        List<Child> children = new List<Child>();
        List<Guardian> guardian = new List<Guardian>();
        Child child;

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            // TXT BOX är du säker på att ta bort?
        }



        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            // query lägga till person i lista beroende på guardian / child

            // text om att ny är tillagd

            // uppdatera list

            //DbOperations.AddNewGuardian();
            //DbOperations.AddNewChild();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // tömm alla fält och selecteditem
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lblStaffFirstname.Content = $"Inloggad som {Activestaff.Firstname} {Activestaff.Lastname}";

            children = DbOperations.GetAllChildren();
            ListViewChildren.ItemsSource = children;
            guardian = DbOperations.GetAllGuardians();
            ListViewGuardians.ItemsSource = guardian;

        }

        private void ListViewGuardians_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Activeguardian.Setactiveguardian((Guardian)ListViewGuardians.SelectedItem);

            }
            catch (Exception)
            {

                return;
            }

            if (guardian != null)
            {
                txtboxFirstNameGuardian.Text = Activeguardian.Firstname;
                txtboxLastNameGuardian.Text = Activeguardian.Lastname;
                txtboxPhoneGuardian.Text = Activeguardian.Phone.ToString();
                txtboxEmailGuardian.Text = Activeguardian.Email;
            }
        }

        private void ListViewChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activechild.Setactivechild((Child)ListViewChildren.SelectedItem);
            txtboxFirstName.Text = Activechild.Firstname;
            txtboxFirstName.Text = Activechild.Lastname;
            txtboxClass.Text = Activechild.Class;
            txtboxGuardian.Text = Activechild.Guardian;
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            // DbOperations.UpdateChildProperties();
           
           // children = DbOperations.GetAllChildren();
           // ListViewChildren.ItemsSource = children;
        }
        private void SaveGuardian_Click(object sender, RoutedEventArgs e)
        {

            Activeguardian.Setactiveguardian((Guardian)ListViewGuardians.SelectedItem);
            DbOperations.UpdateGuardianProperties(Convert.ToInt32(txtboxPhoneGuardian.Text), txtboxEmailGuardian.Text, txtboxFirstNameGuardian.Text, txtboxLastNameGuardian.Text);
            ListViewGuardians.ItemsSource = DbOperations.GetAllGuardians();
        }
    }
}
