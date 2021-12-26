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
using MaterialDesignThemes.Wpf;


namespace PL
{
    /// <summary>
    /// Interaction logic for UsersMainWindow.xaml
    /// </summary>
    public partial class UsersMainWindow : Window
    {
        public UsersMainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Sign in button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            //TO DO
            //if (UserNameTextBox.Text == "admin" && PasswordTextBox.DataContext.ToString() == "admin")
            //    MainWindow.Show(Admin);
            //else if (UserNameTextBox.Text == "user" && PasswordTextBox.DataContext.ToString() == "user")
            //    MainWindow.Show(User);
            //else
            //    MessageBox.Show("User name or password are not correct!");
        }

        /// <summary>
        /// sign up button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            /////////////////////////////////////
            ///TO DO-
            ///Sign up page
        }

        ///// <summary>
        ///// reveal the password
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ShowPasswordCharsCheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    PasswordTextBox.DataContext = PasswordTextBox.Password;
        //    PasswordTextBox.Visibility = System.Windows.Visibility.Collapsed;
        //    MyTextBox.Visibility = System.Windows.Visibility.Visible;

        //    MyTextBox.Focus();
        //}

        ///// <summary>
        ///// bring back the password to char="•"
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ShowPasswordCharsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    PasswordTextBox.DataContext = PasswordTextBox.Password;
        //    PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
        //    MyTextBox.Visibility = System.Windows.Visibility.Collapsed;

        //    PasswordTextBox.Focus();
        //}
    }
}
