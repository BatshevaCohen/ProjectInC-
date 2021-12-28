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
        /// this window opens just after signing up- so the user won't need to insert his username and password again
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public UsersMainWindow(string username, string password)
        {
            InitializeComponent();
            this.UserNameTextBox.Text = username;
            this.PasswordTextBox.Password = password;
        }

        /// <summary>
        /// Sign in button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            //the username and password shoouldn't be empty
            if (AllFieldsRequired())
            {
                new MainWindow().Show();
                this.Close();
            }
            else
                MessageBox.Show("All fields are required to continue");

            //TO DO
            string userType = "user";
            if (UserNameTextBox.Text == "admin" && PasswordTextBox.Password.ToString() == "admin")
            {
                userType = "admin";
                MainWindow.Show(userType);
            }
            else if (UserNameTextBox.Text == "user" && PasswordTextBox.Password.ToString() == "user")
            {
                userType="user";
                MainWindow.Show(userType);
            }
            else
                MessageBox.Show("User name or password are not correct!");
        }

        /// <summary>
        /// checks if all the fileds were filled in
        /// </summary>
        /// <returns></returns>
        private bool AllFieldsRequired()
        {
            if (UserNameTextBox.Text.Count() != 0 && PasswordTextBox.Password.Count() != 0)
                return true;
            return false;
        }

        /// <summary>
        /// sign up button- open the sign up window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            new SignUpWindow().Show();
            this.Close();
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
