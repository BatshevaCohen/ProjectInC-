using IBL.BO;
using IBL;
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
        IBL.BO.IBL myBL = new IBL.BO.BLObject();
        IBL.BO.Drone drone;
        private DroneToList? droneToList;

        /// <summary>
        /// the view of the window
        /// </summary>
        public DroneWindow()
        {
            drone = new IBL.BO.Drone();
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));

            IEnumerable<StationToList> listStationToList = myBL.ShowStationList();
            List<int> stationIDs= new();
            foreach (StationToList station in listStationToList)
            {
                stationIDs.Add(station.Id);
            }
            stationsComboBox.ItemsSource = stationIDs;
        }

        public DroneWindow(IBL.BO.Drone drone)
        {
            drone = new IBL.BO.Drone();
            this.drone = drone;
            DataContext = drone;
            InitializeComponent();
            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            droneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            stationsComboBox.ItemsSource = myBL.ShowStationList();
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
                Location = droneToList.Location,
                 

            };
            if (droneToList.ParcelNumberTransferred != 0)
            {
                Parcel parcel = myBL.GetParcelByDroneId(droneToList.Id);

                drone.ParcelInTransfer = new()
                {
                    Id = droneToList.ParcelNumberTransferred,
                    Priority = parcel.Priority,
                    
                };
                // אם היא סופקה אך איננה שוייכה
                if (parcel.CollectionTime != DateTime.MinValue && parcel.SupplyTime == DateTime.MinValue) 
                    drone.ParcelInTransfer.ParcelTransferStatus = ParcelTransferStatus.WaitingToBePickedUp;
                //בזמן משלוח
                if(parcel.CollectionTime == DateTime.MinValue && parcel.SupplyTime != DateTime.MinValue)
                    drone.ParcelInTransfer.ParcelTransferStatus = ParcelTransferStatus.OnTheWayToDestination;



            }
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
        
        private void droneStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// adds the drone to the BL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {

            int stationId = 12345; ////////////////?????????


            Drone drone = new Drone()
            {
                Id = Int32.Parse(idTextBox.Text),
                Model= droneModelTextBox.Text,
                Battery = Int32.Parse(batteryPrecentTextBox.Text),
                DroneStatuses= (DroneStatuses)droneStatusComboBox.SelectedItem,
                Weight = (Weight)droneWeightComboBox.SelectedItem,
            };
            drone.Location = new Location()
            {
                Latitude = double.Parse(latitudeTextBox.Text),
                Longitude = double.Parse(longitudeTextBox.Text)
            };
            drone.ParcelInTransfer = new()
            { 
                Id = 0 
            };
            myBL.AddDrone(drone, stationId);

            MessageBox.Show("Drone added seccessfuly!");


            //לעשות פונקציה שמאפסת את כל השדות לאחר הוספת הרחפן
            idTextBox.Text = "";
        }
    }
}