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
using Microsoft.VisualBasic;


namespace PL
{
    /// <summary>
    /// Interaction logic for DroneInActionView.xaml
    /// </summary>
    public partial class DroneInActionView : Window
    {
      
        IBL.BO.Drone drone;
        private DroneToList? droneToList;
        IBL.BO.IBL mybl;

        /// <summary>
        /// Add drone window
        /// </summary>
        /// <param name="droneListWindow"></param>
        /// <param name="bL"></param>
        public DroneInActionView(DroneListWindow droneListWindow, IBL.BO.IBL bL)
        {
            InitializeComponent();
            drone = new IBL.BO.Drone();
            DataContext = drone;
            AddGrid.Visibility = Visibility.Visible;
            btnAddDrone.Visibility = Visibility.Visible;


            droneWeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            AddDroneStatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            mybl = bL;
            IEnumerable<StationToList> listStationToList = mybl.ShowStationList();
            List<int> stationIDs = new();
            foreach (StationToList station in listStationToList)
            {
                stationIDs.Add(station.Id);
            }
            stationsComboBox.ItemsSource = stationIDs;
        }

        /// <summary>
        /// Update drone
        /// </summary>
        /// <param name="droneToList"></param>
        /// <param name="bL"></param>
        /// <param name="droneListWindow"></param>
        public DroneInActionView(DroneToList droneToList , IBL.BO.IBL bL, DroneListWindow droneListWindow)
        {
            InitializeComponent();
            mybl = bL;
            UpdateGrid.Visibility = Visibility.Visible;
            ShowDrone.Visibility = Visibility.Visible;
            this.droneToList = droneToList;
            drone = new IBL.BO.Drone()
            {
                Id = droneToList.Id,
                Battery = droneToList.Battery,
                Model = droneToList.Model,
                DroneStatuses = droneToList.DroneStatuses,
                Weight = droneToList.Weight,

                Location = new Location()
                {
                    Latitude = droneToList.Location.Latitude,
                    Longitude = droneToList.Location.Longitude
                }
                
            };
            if(droneToList.ParcelNumberTransferred!=0)
            {
                Parcel parcel = mybl.GetParcelByDroneId(drone.Id);
                ParcelInTransfer_Grid.Visibility = Visibility.Visible;
                drone.ParcelInTransfer= new()
                {
                    Id = parcel.Id,
                    Weight = parcel.Weight,
                    Priority = parcel.Priority

                };
                if (parcel.CollectionTime != DateTime.MinValue)
                {
                    drone.ParcelInTransfer.ParcelTransferStatus = ParcelTransferStatus.WaitingToBePickedUp; 

                }
                if(parcel.CollectionTime==DateTime.MinValue&&parcel.SupplyTime!=DateTime.MinValue)
                {
                    drone.ParcelInTransfer.ParcelTransferStatus = ParcelTransferStatus.OnTheWayToDestination;
                }
                Customer customerReciver = mybl.GetCustomer(parcel.Resiver.Id);
                Customer customerSender = mybl.GetCustomer(parcel.Sender.Id);
                drone.ParcelInTransfer.SupplyTargetLocation = new()
                {
                    Latitude = customerSender.Location.Latitude,
                    Longitude= customerSender.Location.Longitude,
                };
              
                drone.ParcelInTransfer.CollectingLocation = new()
                {
                    Latitude= customerReciver.Location.Latitude,
                    Longitude = customerReciver.Location.Longitude,
                };
                drone.ParcelInTransfer.Reciver = new()
                {
                   Id= customerReciver.Id,
                   Name=customerReciver.Name,
                };
                drone.ParcelInTransfer.Sender = new()
                {
                    Id = customerReciver.Id,
                    Name = customerReciver.Name,
                };
                double distance=  mybl.CalculateDistance(customerReciver.Location.Longitude, customerReciver.Location.Latitude,
                customerSender.Location.Longitude, customerSender.Location.Latitude);
                drone.ParcelInTransfer.TransportDistance = distance;
            }
            else
            {
                ParcelInTransfer_Grid.Visibility = Visibility.Collapsed;

                //להחביא את כל הפקדים הקשורים לחבילה
            }


            DataContext = drone;
            
          
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// update drone's name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDrone_Click(object sender, RoutedEventArgs e)
        {
            String droneName = mybl.GetDrone(Int32.Parse(idTextBox.Text)).Model;
            string newName = droneModelTextBox.Text;
            //only if the name has changed by the user
            if (droneName != droneModelTextBox.Text)
            {
                mybl.UpdateDroneName(Int32.Parse(idTextBox.Text), droneModelTextBox.Text);
                MessageBox.Show("Drone updated seccessfuly!");
                droneModelTextBox.Text = newName;
            }
            else
            {
                MessageBox.Show("Please update the drone's name");
            }
        }

        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            if (droneStatusTxtBox.Text == "Available")
            {
                try
                {
                    mybl.ShowDroneList();
                    mybl.UpdateChargeDrone(Int32.Parse(idTextBox.Text));
                    MessageBox.Show("Drone sent to charge seccessfuly!");
                    droneStatusTxtBox.Text = "Maintenance";
                    btnDroneToCharge.Visibility = Visibility.Hidden;
                    btnCollectParcel.Visibility = Visibility.Hidden;
                    btnDroneToDelivery.Visibility = Visibility.Hidden;
                    btnReleaiseToCharge.Visibility = Visibility.Visible;
                    btnParcelDelivery.Visibility = Visibility.Hidden;
                }
               catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        /// <summary>
        /// discharge drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReleaiseToCharge_Click(object sender, RoutedEventArgs e)
        {
            if (droneStatusTxtBox.Text == "Maintenance")
            {
                //input box- so the user will insert the charging time
                TimeSpan chargingTime = TimeSpan.Parse(Interaction.InputBox("Please enter time of charging", "Time of charging", ""));
                
                try
                {
                    mybl.DischargeDrone(Int32.Parse(idTextBox.Text), chargingTime);
                    MessageBox.Show("Drone discharged seccessfuly!");
                    droneStatusTxtBox.Text = "Available";
                    batteryStatusTextBox.Text = mybl.GetDrone(Int32.Parse(idTextBox.Text)).Battery.ToString();

                    btnDroneToCharge.Visibility = Visibility.Visible;
                    btnDroneToDelivery.Visibility = Visibility.Visible;
                    btnReleaiseToCharge.Visibility = Visibility.Hidden;
                    btnCollectParcel.Visibility = Visibility.Hidden;
                    btnParcelDelivery.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        /// <summary>
        /// sending drone to delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDroneToDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (droneStatusTxtBox.Text == "Available")
            {
                try
                {
                    mybl.UpdateParcelToDrone(Int32.Parse(idTextBox.Text));
                    MessageBox.Show("Drone sent to delivery seccessfuly!");
                    droneStatusTxtBox.Text = "Shipping";
                    btnDroneToCharge.Visibility = Visibility.Hidden;
                    btnDroneToDelivery.Visibility = Visibility.Hidden;
                    btnReleaiseToCharge.Visibility = Visibility.Hidden;
                    btnCollectParcel.Visibility = Visibility.Visible;
                    btnParcelDelivery.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Can't send drone to delivery");
        }

        private void btnCollectParcel_Click(object sender, RoutedEventArgs e)
        {
          
            Drone drone = mybl.GetDrone(Int32.Parse(idTextBox.Text));
            //only if the drone is on Maintenance
            //and the parcel have not been collected yet
            if (drone.ParcelInTransfer != null)
            {
                if (droneStatusTxtBox.Text == "Shipping" && drone.ParcelInTransfer!=null)
                {
                    mybl.UpdateParcelPickUpByDrone(Int32.Parse(idTextBox.Text));
                    MessageBox.Show("Drone pick up the parcel seccessfully!");
                    droneStatusTxtBox.Text = "Shipping";
                    
                    btnDroneToCharge.Visibility = Visibility.Hidden;
                    btnDroneToDelivery.Visibility = Visibility.Hidden;
                    btnReleaiseToCharge.Visibility = Visibility.Hidden;
                    btnCollectParcel.Visibility = Visibility.Visible;
                    btnParcelDelivery.Visibility = Visibility.Visible;
                }
            }
            else
                MessageBox.Show("Can't send drone to pickup parcel");
        }

        /// <summary>
        /// parcel supply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParcelDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (drone.ParcelInTransfer != null)
            {
                if (droneStatusTxtBox.Text == "Shipping" && drone.ParcelInTransfer != null)
                {
                    mybl.UpdateParcelSuppliedByDrone(Int32.Parse(idTextBox.Text));
                    MessageBox.Show("Drone pick up the parcel seccessfully!");
                    droneStatusTxtBox.Text = "Available";
                    btnDroneToCharge.Visibility = Visibility.Visible;
                    btnDroneToDelivery.Visibility = Visibility.Hidden;
                    btnReleaiseToCharge.Visibility = Visibility.Hidden;
                    btnCollectParcel.Visibility = Visibility.Visible;
                    btnParcelDelivery.Visibility = Visibility.Hidden;

                }
            }
            else
                MessageBox.Show("Can't supply parcel by the drone");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Parcel p = mybl.GetParcelByDroneId(drone.Id);

            if (droneStatusTxtBox.Text=="Available")
            {
                btnDroneToCharge.Visibility = Visibility.Visible;
                btnDroneToDelivery.Visibility = Visibility.Visible;
                btnReleaiseToCharge.Visibility = Visibility.Hidden;
                btnCollectParcel.Visibility = Visibility.Hidden;
                btnParcelDelivery.Visibility = Visibility.Hidden;
            }
            else if (droneStatusTxtBox.Text == "Maintenance")
            {
                btnDroneToCharge.Visibility = Visibility.Hidden;
                btnDroneToDelivery.Visibility = Visibility.Hidden;
                btnReleaiseToCharge.Visibility = Visibility.Visible;
                btnCollectParcel.Visibility = Visibility.Hidden;
                btnParcelDelivery.Visibility = Visibility.Hidden;
            }
            // the drone status in shipping
          
            else if(droneStatusTxtBox.Text == "shipping" && drone.ParcelInTransfer.Id != 0 && p.CollectionTime!=DateTime.MinValue)
            {
                btnDroneToCharge.Visibility = Visibility.Hidden;
                btnDroneToDelivery.Visibility = Visibility.Hidden;
                btnReleaiseToCharge.Visibility = Visibility.Hidden;
                btnCollectParcel.Visibility = Visibility.Visible;
                btnParcelDelivery.Visibility = Visibility.Visible;
            }
            else if (droneStatusTxtBox.Text == "shipping" && drone.ParcelInTransfer.Id!=0 && p.CollectionTime==DateTime.MinValue)
            {
                btnDroneToCharge.Visibility = Visibility.Hidden;
                btnDroneToDelivery.Visibility = Visibility.Hidden;
                btnReleaiseToCharge.Visibility = Visibility.Hidden;
                btnCollectParcel.Visibility = Visibility.Visible;
                btnParcelDelivery.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
                    Model = droneModelTextBox.Text,
                    Battery = Int32.Parse(batteryPrecentTextBox.Text),
                    DroneStatuses = DroneStatuses.Maintenance,
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
                mybl.AddDrone(drone, stationId);

                MessageBox.Show("Drone added seccessfuly!");


                //לעשות פונקציה שמאפסת את כל השדות לאחר הוספת הרחפן
                idTextBox.Text = "";
            }
        }
    }

