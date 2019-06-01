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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(txtboxPhone.Text, out int phone);
            string email = txtboxEmail.Text;
            int id = Activeguardian.Id;
            string firstname = txtboxFirstName.Text;
            string lastname = txtboxLastName.Text;

            DbOperations.UpdateGuardianProperties(phone, email, firstname, lastname);

            Activeguardian.Phone = phone.ToString();
            Activeguardian.Email = email;

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
