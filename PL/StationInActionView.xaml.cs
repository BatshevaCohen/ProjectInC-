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
        BO.IBL mybl;
        public StationInActionView()
        {
            InitializeComponent();
        }
        public StationInActionView(StationToList stationToL, BO.IBL bL, StationListWindow stationListWindow)
        {
            InitializeComponent();
            mybl = bL;
            station = new Station();
            this.StationToList = stationToL;
            UpdateGrid.Visibility = Visibility.Visible;
            station = new()
            {
                Id= stationToL.Id,
                Name= stationToL.Name,
                AvailableChargingSpots=stationToL.AvailableChargingSpots
            };

            Location location = mybl.GetStation(stationToL.Id).Location;
            station.Location = new()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            List<DroneInCharging> droneInCharging = mybl.GetStation(stationToL.Id).droneInChargings;
            if (station.droneInChargings.Count() > 0)
            {
                foreach (DroneInCharging item in droneInCharging)
                {
                    station.droneInChargings.Add(item);
                }
            }
            else
            {
                MessageBox.Show("bla bla");
            }
            btnAddStation.Visibility = Visibility.Visible;
            btnUpdateStation.Visibility = Visibility.Visible;
            DataContext = station;

        }

        private void btnUpdateStation_Click(object sender, RoutedEventArgs e)
        {

            String StationName = mybl.GetStation(Int32.Parse(stationIdTextBox.Text)).Name;
            string newName = stationIdTextBox.Text;
            //only if the name has changed by the user
            if (StationName != NameTextBox.Text)
            {
                mybl.UpdateStetion(Int32.Parse(stationIdTextBox.Text), StationName, Int32.Parse(AvailableChargingSpotsTextBox.Text));
                MessageBox.Show("Drone updated seccessfuly!");
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
                Id = Int32.Parse(stationIdTextBox.Text),
                Name = (NameTextBox.Text),
                AvailableChargingSpots = Int32.Parse(AvailableChargingSpotsTextBox.Text),


            };
            station.Location = new()
            {
                Latitude = double.Parse(latitudeTextBox.Text),
                Longitude = double.Parse(longitudeTextBox.Text),
            };

            mybl.AddStation(station);
        }
    }
}
