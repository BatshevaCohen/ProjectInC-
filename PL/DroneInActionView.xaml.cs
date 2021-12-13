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
using Microsoft.VisualBasic;


namespace PL
{
    
    /// <summary>
    /// Interaction logic for DroneInActionView.xaml
    /// </summary>
    public partial class DroneInActionView : Window
    {
        //  private DroneToList? droneToList;
        IBL.BO.IBL myBL = new IBL.BO.BLObject();
        IBL.BO.Drone drone;
        private DroneToList? droneToList;

        public DroneInActionView()
        {
            drone = new IBL.BO.Drone();
            DataContext = drone;
            InitializeComponent();
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
                Location=droneToList.Location,
            };
            DataContext = drone;
            InitializeComponent();
          
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
            String droneName = myBL.GetDrone(Int32.Parse(idTextBox.Text)).Model;
            string newName = droneModelTextBox.Text;
            //only if the name has changed by the user
            if (droneName != droneModelTextBox.Text)
            {
                myBL.UpdateDroneName(Int32.Parse(idTextBox.Text), droneModelTextBox.Text);
                MessageBox.Show("Drone updated seccessfuly!");
                droneModelTextBox.Text = newName;
            }
            else
                MessageBox.Show("Please update the drone's name");
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
                myBL.UpdateChargeDrone(Int32.Parse(idTextBox.Text));
                MessageBox.Show("Drone sent to charge seccessfuly!");
                droneStatusTxtBox.Text = "Maintenance";
            }
            else
                MessageBox.Show("Drone can't be sent to charge!");
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
                myBL.DischargeDrone(Int32.Parse(idTextBox.Text), chargingTime);
                MessageBox.Show("Drone discharged seccessfuly!");
                droneStatusTxtBox.Text = "Available";
                batteryStatusTextBox.Text = myBL.GetDrone(Int32.Parse(idTextBox.Text)).Battery.ToString();
            }
            else
                MessageBox.Show("Drone can't be discharged!");
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
                myBL.UpdateParcelToDrone(Int32.Parse(idTextBox.Text));
                MessageBox.Show("Drone sent to delivery seccessfuly!");
                droneStatusTxtBox.Text = "Shipping";
            }
            else
                MessageBox.Show("Can't send drone to delivery");
        }

        private void btnCollectParcel_Click(object sender, RoutedEventArgs e)
        {
            Drone drone = myBL.GetDrone(Int32.Parse(idTextBox.Text));
            //only if the drone is on Maintenance status and the parcel have not been collected yet
            if (droneStatusTxtBox.Text == "Shipping" && drone.ParcelInTransfer.ParcelTransferStatus==ParcelTransferStatus.WaitingToBePickedUp)
            {
                myBL.UpdateParcelPickUpByDrone(Int32.Parse(idTextBox.Text));
                MessageBox.Show("Drone pick up the parcel seccessfully!");
                droneStatusTxtBox.Text = "Shipping";
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
            if (droneStatusTxtBox.Text == "Shipping" && drone.ParcelInTransfer.ParcelTransferStatus == ParcelTransferStatus.OnTheWayToDestination)
            {
                myBL.UpdateParcelSuppliedByDrone(Int32.Parse(idTextBox.Text));
                MessageBox.Show("Drone pick up the parcel seccessfully!");
                droneStatusTxtBox.Text = "Available";
            }
            else
                MessageBox.Show("Can't supply parcel by the drone");
        }
    }
}

