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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationInActionView.xaml
    /// </summary>
    public partial class StationInActionView : Window
    {
        BO.Station station;
        private StationToList? StationToList;
        IBL mybl;
        public StationInActionView()
        {
            InitializeComponent();
            
        }
        public StationInActionView(StationToList stationToL, IBL bL, StationListWindow stationListWindow)
        {
            InitializeComponent();
            mybl = bL;
            DataContext = stationToL;
            this.StationToList = stationToL;
            btnUpdateStation.Visibility = Visibility.Visible;
            UpdateGrid.Visibility = Visibility.Visible;
            station = new()
            {
                Id = stationToL.Id,
                Name = stationToL.Name,
                AvailableChargingSpots = stationToL.AvailableChargingSpots
            };

            station.droneInChargings = mybl.GetStation(stationToL.Id).droneInChargings;

            if (station.droneInChargings.Count() > 0)
            {
                lable_droneincharging.Visibility = Visibility.Visible;
                foreach (DroneInCharging item in station.droneInChargings)
                {
                    listVDtoneInCharging.Visibility = Visibility.Visible;
                    ListViewItem newItem = new ListViewItem();
                    newItem.Content = item;
                    listVDtoneInCharging.Items.Add(newItem);
                }
            }
            else
            {
                listVDtoneInCharging.Visibility = Visibility.Collapsed;
            }
            DataContext = station;

        }
        public StationInActionView(StationListWindow stationListWindow, IBL bL)
        {
            mybl = bL;
            InitializeComponent();
            btnAddStation.Visibility = Visibility.Visible;
            AddGridStation.Visibility = Visibility.Visible;
            DataContext = station;
            stationListWindow.StationsListView.Items.Refresh();
        }

        private void btnUpdateStation_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateStation.Visibility = Visibility.Visible;
            UpdateGrid.Visibility = Visibility.Visible;
            String StationName = mybl.GetStation(Int32.Parse(stationIdTextBox.Text)).Name;
            string newName = stationIdTextBox.Text;
            //only if the name has changed by the user
            if (StationName != NameTextBox.Text)
            {
                mybl.UpdateStetion(Int32.Parse(stationIdTextBox.Text), StationName, Int32.Parse(AvailableChargingSpotsTextBox.Text));
                MessageBox.Show("Station updated seccessfuly!");
                stationIdTextBox.Text = newName;
            }
            else
            {
                MessageBox.Show("Please update the Station's name");
            }
        }


        private void btnAddeStation_Click(object sender, RoutedEventArgs e)
        {

            Station station = new Station()
            {
                Id = Int32.Parse(stationIdTextBoxadd.Text),
                Name = (NameTextBoxadd.Text),
                AvailableChargingSpots = Int32.Parse(AvailableChargingSpotsTextBoxadd.Text),
            };
            station.Location = new()
            {
                Latitude = double.Parse(latitudeTextBoxadd.Text),
                Longitude = double.Parse(longitudeTextBoxadd.Text),
            };
            try
            {
                mybl.AddStation(station);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StationInActionView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneInCharging droneInCharging = listVDtoneInCharging.SelectedItem as DroneInCharging;
            Drone d = mybl.GetDrone(droneInCharging.Id);
            DroneToList droneTo = new DroneToList()
            {
                Id = d.Id,
                Battery = d.Battery,
                Model = d.Model,
                DroneStatuses = d.DroneStatuses,
                Weight = d.Weight,
                ParcelNumberTransferred = 0,
            };
            droneTo.Location = new()
            {
                Latitude = d.Location.Latitude,
                Longitude = d.Location.Longitude,
            };

            new DroneInActionView(droneTo, mybl).Show();
        }

    }
}
