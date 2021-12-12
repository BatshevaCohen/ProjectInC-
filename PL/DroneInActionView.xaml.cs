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
    /// Interaction logic for DroneInActionView.xaml
    /// </summary>
    public partial class DroneInActionView : Window
    {
        //  private DroneToList? droneToList;

        IBL.BO.Drone drone;
        private DroneToList? droneToList;

        public DroneInActionView()
        {
            drone = new IBL.BO.Drone();
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }
        public DroneInActionView(DroneToList droneToList)
        {
            this.droneToList = droneToList;
            drone = new IBL.BO.Drone()
            {
                Id = droneToList.Id,
                Battery = droneToList.Battery,
                Model = droneToList.Model,
                DroneStatuses = droneToList.DroneStatuses,
                Weight = droneToList.Weight,
            };
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

