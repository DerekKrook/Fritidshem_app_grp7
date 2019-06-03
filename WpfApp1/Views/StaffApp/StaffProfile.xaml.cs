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
    /// Interaction logic for StaffProfile.xaml
    /// </summary>
    public partial class StaffProfile : Window
    {
        public StaffProfile()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string email = txtboxEmail.Text;

            DbOperations.UpdateStaffProperties(email);

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
