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
            myBL.UpdateDroneName(Int32.Parse(idTextBox.Text), droneModelTextBox.Text);
            MessageBox.Show("Drone updated seccessfuly!");

        }

        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            myBL.UpdateChargeDrone(Int32.Parse(idTextBox.Text));
            MessageBox.Show("Drone sent to charge seccessfuly!");
        }
        /// <summary>
        /// discharge drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReleaiseToCharge_Click(object sender, RoutedEventArgs e)
        {
            //input box- so the user will insert the charging time
            TimeSpan chargingTime = TimeSpan.Parse(Interaction.InputBox("Please enter time of charging", "Time of charging", ""));
            myBL.DischargeDrone(Int32.Parse(idTextBox.Text), chargingTime);
        }
    }
}

