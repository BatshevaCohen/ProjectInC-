using System.Windows;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// main window with user type- "user" or "admin"
    /// each user type have a different mainWindow show
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBL;


        /// <summary>
        /// TEMPERARY CONSTRACTOR FOR DEBUGGING
        /// </summary>
        public MainWindow()
        {
            myBL = BlFactory.GetBl();
            InitializeComponent();
        }

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="userType"></param>
        public MainWindow(BO.Permit userType)
        {
            myBL = BlFactory.GetBl();
            InitializeComponent();
        }

        /// <summary>
        /// click: show the list of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDronesList_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow wnd = new DroneListWindow(myBL);
            wnd.Show();
        }
        /// <summary>
        /// click: show the list of the stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowStationList_Click(object sender, RoutedEventArgs e)
        {
           StationListWindow wnd = new StationListWindow(myBL);
            wnd.Show();
        }

        private void btnShowCustumersList_Click(object sender, RoutedEventArgs e)
        {
            CustumerListWindow wnd = new CustumerListWindow(myBL);
            wnd.Show();
        }

        private void btnShowParcelesList_Click(object sender, RoutedEventArgs e)
        {
            ParcelListWindowe wnd = new ParcelListWindowe(myBL);
            wnd.Show();
        }
    }
    
}
