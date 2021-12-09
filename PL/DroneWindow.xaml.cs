using IBL.BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.BO.Drone drone;
        public DroneWindow()
        {
            drone = new IBL.BO.Drone();
            DataContext = drone;
            InitializeComponent();
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight));
        }

        public DroneWindow(IBL.BO.Drone drone)
        {
            this.drone = drone;
            DataContext = drone;
            InitializeComponent();
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        public Drone Drone { get => drone; }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            String msg = drone.ToString();
            MessageBox.Show(msg);
        }
    }
}
