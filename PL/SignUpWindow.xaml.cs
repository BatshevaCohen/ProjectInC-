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
using System.Diagnostics;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using BlApi;
using DalApi;
using DO;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        /// <summary>
        /// initializing
        /// </summary>
        public SignUpWindow()
        {
            InitializeComponent();
            enterDetailsGrid.Visibility = Visibility.Visible;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        /// <summary>
        /// checks if all the fileds were filled in
        /// </summary>
        /// <returns></returns>
        private bool AllFieldsRequired()
        {
            if (FirstNameTextBox.Text.Count() != 0 && 
                LastNameTextBox.Text.Count() != 0 && 
                IDTextBox.Text.Count() != 0 &&
                PhoneTextBox.Text.Count() != 0 &&
                LongitudeTextBox.Text.Count() != 0 &&
                LatitudeTextBox.Text.Count() != 0)
                return true;
            return false;
        }

        /// <summary>
        /// SignUp_Final_Button_Click- when finished entering the new user details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_Final_Button_Click(object sender, RoutedEventArgs e)
        {
            if (AllFieldsRequired())
            {
                enterDetailsGrid.Visibility = Visibility.Collapsed;
                UserNameAndPassword_Grid.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("All fields are required to continue");
        }

        /// <summary>
        /// check that the text box includes numberic values only- you can't enter something that isn't digit
        /// from:https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// check that the text box includes letters only- you can't enter something that isn't letters
        /// from:https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlphabetValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// check that the text box includes letters only- you can't enter something that isn't letters
        /// from:https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// after choosing username and password---- add the customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalUserNamePassword_Button_Click(object sender, RoutedEventArgs e)
        {
            //only if the username ad password inserted
            if (ChooseUserNameTextBox.Text.Count() != 0 && ChoosePasswordTextBox.Text.Count() != 0)
            {
                //add the new customer to the BO
                BO.Customer customer = new BO.Customer()
                {
                    Id = Int32.Parse(IDTextBox.Text),
                    Name = FirstNameTextBox.Text + LastNameTextBox.Text,
                    Phone = PhoneTextBox.Text,
                };
                customer.Location = new BO.Location()
                {
                    Longitude = double.Parse(LongitudeTextBox.Text),
                    Latitude = double.Parse(LatitudeTextBox.Text),
                };
                //new customer doesn't have any parcels
                customer.SentParcels = null;
                customer.ReceiveParcels = null;

                //keep the username and password in the memory

                //go to sign in page again
                string username = ChooseUserNameTextBox.Text;
                string password = ChoosePasswordTextBox.Text;
                new UsersMainWindow(username, password).Show();
                this.Close();
            }
            else
                MessageBox.Show("All fields are required to continue");
        }
    }
}
