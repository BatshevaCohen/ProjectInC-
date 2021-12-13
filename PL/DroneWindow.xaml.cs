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
        private DroneToList? droneToList;

        public DroneWindow()
        {
            drone = new IBL.BO.Drone();
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        public DroneWindow(IBL.BO.Drone drone)
        {
            drone = new IBL.BO.Drone();
            this.drone = drone;
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }
       
        public DroneWindow(DroneToList droneToList)
        {
            this.droneToList = droneToList;
            drone = new IBL.BO.Drone()
            {
                Id = droneToList.Id,
                Battery = droneToList.Battery,
                Model = droneToList.Model, 
                DroneStatuses = droneToList.DroneStatuses,
                Weight = droneToList.Weight,
                Location=droneToList.Location
            };
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        public Drone Drone { get => drone; }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            String msg = drone.ToString();
            MessageBox.Show(msg);
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}