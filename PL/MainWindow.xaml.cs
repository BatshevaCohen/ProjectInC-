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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BO.IBL myBL;
        public MainWindow()
        {
            myBL = BlFactory.GetBl();
            InitializeComponent();
           
        }

        private void btnShowDronesList_Click(object sender, RoutedEventArgs e)
        {
            DroneListWindow wnd = new DroneListWindow(myBL);
            wnd.Show();
        }
        private void btnShowStationList_Click(object sender, RoutedEventArgs e)
        {
           StationListWindow wnd = new StationListWindow(myBL);
            wnd.Show();
        }
    }
}
